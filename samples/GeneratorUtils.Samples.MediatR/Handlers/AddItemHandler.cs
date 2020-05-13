using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Handlers
{
    public sealed class AddItemHandler : IRequestHandler<Requests.Items.AddItem, Guid>
    {
        public Task<Guid> Handle(Requests.Items.AddItem request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
