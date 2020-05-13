using MediatR;
using System;

namespace GeneratorUtils.Samples.Api.Requests.Items
{
    public sealed class AddItem : IRequest<Guid>
    {
    }
}
