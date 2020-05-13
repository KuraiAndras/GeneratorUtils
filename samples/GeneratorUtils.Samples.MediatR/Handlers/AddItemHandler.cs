
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
