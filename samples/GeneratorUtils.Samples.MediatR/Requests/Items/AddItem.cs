using MediatR;
using System;

namespace GeneratorUtils.Samples.MediatR.Requests.Items
{
    public sealed class AddItem : IRequest<Guid>
    {
    }
}
