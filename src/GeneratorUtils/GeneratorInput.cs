using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeneratorUtils
{
    public sealed class GeneratorInput
    {
        public GeneratorInput(ISet<TypeInput> inputTypes)
        {
            InputTypes = ImmutableHashSet.Create(inputTypes.ToArray());
        }

        public IImmutableSet<TypeInput> InputTypes { get; }
    }
}