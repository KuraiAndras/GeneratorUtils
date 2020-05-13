using GeneratorUtils.Samples.MediatR.Requests.Items;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Handlers
{
    public sealed class AddItemHandler : IRequestHandler<AddItem, Guid>
    {
        public Task<Guid> Handle(AddItem request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
