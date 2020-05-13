using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator.Templates
{
    public sealed class HandlerTemplate : IRequestHandler<IRequestTemplate, IResponseTemplate>
    {
        public Task<IResponseTemplate> Handle(IRequestTemplate request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
