using GeneratorUtils.Cli.Requests.MediatR;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Cli.Handlers.MediatR
{
    public sealed class GenerateMediatRRequestHandlersHandler : IRequestHandler<GenerateMediatRRequestHandlers>
    {
        private readonly ILogger<GenerateMediatRRequestHandlersHandler> _logger;

        public GenerateMediatRRequestHandlersHandler(ILogger<GenerateMediatRRequestHandlersHandler> logger) => _logger = logger;

        public Task<Unit> Handle(GenerateMediatRRequestHandlers request, CancellationToken cancellationToken)
        {
            Assembly targetAssembly;
            try
            {
#pragma warning disable S3885 // "Assembly.Load" should be used
                targetAssembly = Assembly.LoadFile(request.AssemblyPath);
#pragma warning restore S3885 // "Assembly.Load" should be used
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Assembly load failed at path {0}", request.AssemblyPath);
                return Unit.Task;
            }

            targetAssembly.GetTypes();

            return Unit.Task;
        }
    }
}
