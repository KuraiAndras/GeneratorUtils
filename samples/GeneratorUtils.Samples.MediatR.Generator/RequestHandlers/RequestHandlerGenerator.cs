using System;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator.RequestHandlers
{
    public sealed class RequestHandlerGenerator : MediatRGeneratorBase
    {
        public override Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory)
        {
            // ReSharper disable once MissingIndent
            const string file =
@"using {2};
using {3};
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace {4}
{
    public sealed class {0}Handler : IRequestHandler<{0}, {1}>
    {
        public Task<{1}> Handle({0} request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}";
            var responseType = GetMediatRResponseType(inputType);

            var targetNamespace = GetMediatRTargetNamespace(inputType, "Handlers");

            var path = GetMediatRFilePath(
                targetNamespace,
                "GeneratorUtils.Samples.Api.Handlers.",
                rootDirectory,
                "Handlers",
                inputType.Name + "Handler.cs");

            var tokens = new[]
            {
                inputType.Name,
                responseType.Name,
                inputType.Namespace ?? throw new GeneratorException("No namespace for type"),
                responseType.Namespace ?? throw new GeneratorException("No namespace for type"),
                targetNamespace,
            };

            return Task.FromResult(new FileOutput(file, path, tokens));
        }
    }
}
