﻿using GeneratorUtils.Samples.Api.Domain;
using MediatR;
using System.Collections.Immutable;

namespace GeneratorUtils.Samples.Api.Requests.Orders
{
    public sealed class AddOrder : IRequest
    {
        public IImmutableSet<Item> Items { get; set; } = ImmutableHashSet<Item>.Empty;
    }
}
