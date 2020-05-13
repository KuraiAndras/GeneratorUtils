using GeneratorUtils.Samples.MediatR.Requests.Items;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator.FileGenerators
{
    public sealed class MediatRRequestHandlerTypeProvider : IInputTypeProvider
    {
        public Task<ImmutableArray<TypeInput>> GetInputTypesAsync()
        {
            return Task.FromResult(ImmutableArray.Create(new TypeInput(typeof(AddItem), typeof(MediatRRequestHandlerGenerator))));
        }
    }
}
