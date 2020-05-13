using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class FileGeneratorService : IGeneratorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GeneratorOptions _options;
        private readonly ILogger<FileGeneratorService> _logger;

        public FileGeneratorService(
            IServiceProvider serviceProvider,
            GeneratorOptions options,
            ILogger<FileGeneratorService> logger)
        {
            _serviceProvider = serviceProvider;
            _options = options;
            _logger = logger;
        }

        public async Task GenerateFilesAsync()
        {
            try
            {
                _logger.LogInformation("File generation started");

                var stopwatch = Stopwatch.StartNew();

                var inputTasks = _serviceProvider.GetRequiredService<IEnumerable<IInputTypeProvider>>().Select(p => p.GetInputTypesAsync()).ToArray();
                await Task.WhenAll(inputTasks);

                _logger.LogInformation("Creating file outputs");

                var outputs = new List<FileOutput>();
                foreach (var typeInput in inputTasks.SelectMany(t => t.Result))
                {
                    var handler = _serviceProvider
                        .GetRequiredService<IEnumerable<IFileGenerator>>()
                        .SingleOrDefault(g => g.GetType() == typeInput.GeneratorType);
                    if (handler is null) throw new GeneratorException($"Did not register handler {typeInput.GeneratorType.FullName}");

                    var output = await handler.CreateFileBodyForFileAsync(typeInput.InputType, _options.TargetRootPath);

                    outputs.Add(output);
                }

                _logger.LogInformation("Generating file outputs");

                var fileCount = 0;
                foreach (var fileOutput in outputs)
                {
                    if (File.Exists(fileOutput.FilePath))
                    {
                        if (fileOutput.ReplaceIfExists)
                        {
                            File.Delete(fileOutput.FilePath);
                        }
                        else
                        {
                            _logger.LogInformation("Skipped file {0}", fileOutput.FilePath);
                            continue;
                        }
                    }

                    var tokenizer = _serviceProvider
                        .GetRequiredService<IEnumerable<ITokenizer>>()
                        .SingleOrDefault(t => t.GetType() == fileOutput.Tokenizer);
                    if (tokenizer is null) throw new GeneratorException($"Tokenizer not found {fileOutput.Tokenizer.FullName}");

                    var tokenizedFileBody = await tokenizer.TokenizeAsync(fileOutput.FileBody, fileOutput.Tokens, fileOutput.StringComparison);

                    var fileDirectory = Path.GetDirectoryName(fileOutput.FilePath);
                    if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

                    await using var stream = new FileStream(fileOutput.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    await using var writer = new StreamWriter(stream);
                    await writer.WriteAsync(tokenizedFileBody);

                    _logger.LogInformation("Created file {0}", fileOutput.FilePath);
                    fileCount++;
                }

                stopwatch.Stop();
                _logger.LogInformation("File generation finished");
                _logger.LogInformation("Created {0} files in {1}", fileCount, stopwatch.Elapsed);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "File generation failed");
            }
        }
    }
}
