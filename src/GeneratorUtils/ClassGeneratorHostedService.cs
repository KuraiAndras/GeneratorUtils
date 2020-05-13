using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class ClassGeneratorHostedService : IHostedService
    {
        private readonly GeneratorOptions _options;
        private readonly IImmutableSet<ITokenizer> _tokenizers;
        private readonly IImmutableSet<IFileGenerator> _fileGenerators;
        private readonly IImmutableSet<IInputTypeProvider> _typeProviders;

        public ClassGeneratorHostedService(
            GeneratorOptions options,
            IEnumerable<IInputTypeProvider> typeProviders,
            IEnumerable<IFileGenerator> fileGenerators,
            IEnumerable<ITokenizer> tokenizers)
        {
            var generators = fileGenerators.ToArray();
            var providers = typeProviders.ToArray();
            var tokenizerArray = tokenizers.ToArray();

            if (generators.Length == 0) throw new GeneratorException("No file generators registered");
            if (providers.Length == 0) throw new GeneratorException("No type providers registered");

            _options = options;
            _fileGenerators = ImmutableHashSet.Create(generators);
            _typeProviders = ImmutableHashSet.Create(providers);
            _tokenizers = ImmutableHashSet.Create(tokenizerArray);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var inputTasks = _typeProviders.Select(p => p.GetInputTypesAsync()).ToArray();

            await Task.WhenAll(inputTasks);

            var outputs = new List<FileOutput>();
            foreach (var typeInput in inputTasks.SelectMany(t => t.Result))
            {
                if (cancellationToken.IsCancellationRequested) return;

                var handler = _fileGenerators.SingleOrDefault(g => g.GetType() == typeInput.HandlerType);
                if (handler is null) throw new GeneratorException($"Did not register handler {typeInput.HandlerType.FullName}");

                var output = await handler.CreateFileBodyForFileAsync(typeInput.InputType, _options.TargetRootPath);

                outputs.Add(output);
            }

            foreach (var fileOutput in outputs)
            {
                if (cancellationToken.IsCancellationRequested) return;

                var tokenizer = _tokenizers.SingleOrDefault(t => t.GetType() == fileOutput.Tokenizer) ?? throw new GeneratorException($"Tokenizer not found {fileOutput.Tokenizer.FullName}");

                var tokenizedFileBody = await tokenizer.TokenizeAsync(fileOutput.FileBody, fileOutput.Tokens, fileOutput.StringComparison);

                var fileDirectory = Path.GetDirectoryName(fileOutput.FilePath);
                if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

                await using var stream = new FileStream(fileOutput.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await using var writer = new StreamWriter(stream);

                await writer.WriteAsync(tokenizedFileBody);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
