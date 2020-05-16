using System;
using System.Collections.Generic;

namespace GeneratorUtils.Samples.Api.Domain
{
    public sealed class Order
    {
        public Guid Id { get; set; }

        public ISet<Item> Items { get; } = new HashSet<Item>();
    }
}
