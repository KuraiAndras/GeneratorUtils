using GeneratorUtils.Samples.Api.Domain;
using System.Collections.Immutable;

namespace GeneratorUtils.Samples.Api.Requests.Items
{
    public sealed class ItemsResponse
    {
        public IImmutableSet<Item> Items { get; set; } = ImmutableHashSet<Item>.Empty;
    }
}
