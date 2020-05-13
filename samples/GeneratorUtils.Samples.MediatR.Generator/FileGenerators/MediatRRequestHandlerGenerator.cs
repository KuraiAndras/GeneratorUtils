using MediatR;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator.FileGenerators
{
    public sealed class MediatRRequestHandlerGenerator : IFileGenerator
    {
        public Task<FileOutput> CreateFileBodyForFileAsync(Type type, string rootDirectory)
        {
            var file =
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
            var path = Path.Combine(rootDirectory, "Handlers", type.Name + "Handler.cs");
            var tokens = new[]
            {
                type.Name + "Handler",
                type.FullName,
                type.GetInterface(nameof(IRequest))?.GetGenericArguments().First().FullName ?? "Unit",
            };

            return Task.FromResult(new FileOutput(file, path, tokens));
        }
    }
}
