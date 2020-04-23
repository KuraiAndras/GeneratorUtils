using GeneratorUtils.Cli.Requests.MediatR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Cli.Handlers.MediatR
{
    public sealed class GenerateMediatRRequestHandlersHandler : IRequestHandler<GenerateMediatRRequestHandlers>
    {
        public Task<Unit> Handle(GenerateMediatRRequestHandlers request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
