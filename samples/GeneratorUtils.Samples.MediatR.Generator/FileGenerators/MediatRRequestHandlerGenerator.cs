using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator.FileGenerators
{
    public sealed class MediatRRequestHandlerGenerator : IFileGenerator
    {
        public Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory)
        {
            // ReSharper disable once MissingIndent
            const string file =
@"using MediatR;
using System.Threading;
using System.Threading.Tasks;
using {3};
using {4};

namespace GeneratorUtils.Samples.Api.Handlers
{
    public sealed class {0} : IRequestHandler<{1}, {2}>
    {
        public Task<{2}> Handle({1} request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}";

            Type? responseType = null;
            foreach (var inputTypeInterface in inputType.GetInterfaces())
            {
                if (inputTypeInterface.IsGenericType && inputTypeInterface.GetGenericTypeDefinition() == typeof(IRequest<>))
                {
                    responseType = inputTypeInterface.GetGenericArguments()[0];
                    break;
                }

                if (inputTypeInterface == typeof(IRequest))
                {
                    responseType = typeof(Unit);
                    break;
                }
            }

            if (responseType is null) throw new GeneratorException("No response type");

            var path = Path.Combine(rootDirectory, "Handlers", inputType.Name + "Handler.cs");
            var tokens = new[]
            {
                inputType.Name + "Handler",
                inputType.Name ?? throw new GeneratorException("No full name for type"),
                responseType.Name,
                inputType.Namespace ?? throw new GeneratorException("No namespace for type"),
                responseType.Namespace ?? throw new GeneratorException("No namespace for type"),
            };

            return Task.FromResult(new FileOutput(file, path, tokens));
        }
    }
}
