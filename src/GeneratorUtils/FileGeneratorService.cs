using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class FileGeneratorService : IGeneratorService
    {
        private readonly GeneratorOptions _options;
        private readonly ILogger<FileGeneratorService> _logger;
        private readonly IImmutableSet<ITokenizer> _tokenizers;
        private readonly IImmutableSet<IFileGenerator> _fileGenerators;
        private readonly IImmutableSet<IInputTypeProvider> _typeProviders;

        public FileGeneratorService(
            GeneratorOptions options,
            IEnumerable<IInputTypeProvider> typeProviders,
            IEnumerable<IFileGenerator> fileGenerators,
            IEnumerable<ITokenizer> tokenizers,
            ILogger<FileGeneratorService> logger)
        {
            var generators = fileGenerators.ToArray();
            var providers = typeProviders.ToArray();
            var tokenizerArray = tokenizers.ToArray();

            if (generators.Length == 0) throw new GeneratorException("No file generators registered");
            if (providers.Length == 0) throw new GeneratorException("No type providers registered");

            _options = options;
            _logger = logger;
            _fileGenerators = ImmutableHashSet.Create(generators);
            _typeProviders = ImmutableHashSet.Create(providers);
            _tokenizers = ImmutableHashSet.Create(tokenizerArray);
        }

        public async Task GenerateFilesAsync()
        {
            try
            {
                _logger.LogInformation("File generation started");
                var inputTasks = _typeProviders.Select(p => p.GetInputTypesAsync()).ToArray();

                await Task.WhenAll(inputTasks);

                _logger.LogInformation("Creating file outputs");
                var outputs = new List<FileOutput>();
                foreach (var typeInput in inputTasks.SelectMany(t => t.Result))
                {
                    var handler = _fileGenerators.SingleOrDefault(g => g.GetType() == typeInput.GeneratorType);
                    if (handler is null)
                        throw new GeneratorException($"Did not register handler {typeInput.GeneratorType.FullName}");

                    var output = await handler.CreateFileBodyForFileAsync(typeInput.InputType, _options.TargetRootPath);

                    outputs.Add(output);
                }

                _logger.LogInformation("Generating file outputs");
                foreach (var fileOutput in outputs)
                {
                    var tokenizer = _tokenizers.SingleOrDefault(t => t.GetType() == fileOutput.Tokenizer) ?? throw new GeneratorException($"Tokenizer not found {fileOutput.Tokenizer.FullName}");

                    var tokenizedFileBody = await tokenizer.TokenizeAsync(fileOutput.FileBody, fileOutput.Tokens, fileOutput.StringComparison);

                    var fileDirectory = Path.GetDirectoryName(fileOutput.FilePath);
                    if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

                    await using var stream =
                        new FileStream(fileOutput.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    await using var writer = new StreamWriter(stream);

                    await writer.WriteAsync(tokenizedFileBody);
                }

                _logger.LogInformation("File generation finished");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "File generation failed");
            }
        }
    }
}
