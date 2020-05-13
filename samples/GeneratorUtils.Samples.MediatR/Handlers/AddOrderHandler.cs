using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Handlers
{
    public sealed class AddOrderHandler : IRequestHandler<Requests.Orders.AddOrder, Unit>
    {
        public Task<Unit> Handle(Requests.Orders.AddOrder request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
