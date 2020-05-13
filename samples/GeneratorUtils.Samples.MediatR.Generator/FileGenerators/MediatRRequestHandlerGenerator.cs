using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator.FileGenerators
{
    public sealed class MediatRRequestHandlerGenerator : IFileGenerator
    {
        public Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory)
        {
            // ReSharper disable once MissingIndent
            const string file =
@"
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Handlers
{
    public sealed class {0} : IRequestHandler<{1}, {2}>
    {
        public Task<{2}> Handle({1} request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
";
            var responseString = string.Empty;
            foreach (var inputTypeInterface in inputType.GetInterfaces())
            {
                if (inputTypeInterface.IsGenericType && inputTypeInterface.GetGenericTypeDefinition() == typeof(IRequest<>))
                {
                    responseString = inputTypeInterface.GetGenericArguments()[0].Name;
                    break;
                }

                if (inputTypeInterface == typeof(IRequest))
                {
                    responseString = nameof(Unit);
                    break;
                }
            }

            var path = Path.Combine(rootDirectory, "Handlers", inputType.Name + "Handler.cs");
            var tokens = new[]
            {
                inputType.Name + "Handler",
                inputType.FullName ?? throw new GeneratorException("No full name for type"),
                responseString,
            };

            return Task.FromResult(new FileOutput(file, path, tokens));
        }
    }
}
