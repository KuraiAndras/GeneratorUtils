using GeneratorUtils.Samples.MediatR.Requests.Items;
using MediatR;
using System.Collections.Immutable;

namespace GeneratorUtils.Samples.MediatR.Requests.Orders
{
    public sealed class AddOrder : IRequest
    {
        public IImmutableSet<Item> Items { get; set; } = ImmutableHashSet<Item>.Empty;
    }
}
