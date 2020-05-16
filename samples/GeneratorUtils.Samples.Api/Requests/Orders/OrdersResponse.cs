﻿using GeneratorUtils.Samples.Api.Domain;
using System.Collections.Immutable;

namespace GeneratorUtils.Samples.Api.Requests.Orders
{
    public sealed class OrdersResponse
    {
        public IImmutableSet<Order> Orders { get; set; } = ImmutableHashSet<Order>.Empty;
    }
}