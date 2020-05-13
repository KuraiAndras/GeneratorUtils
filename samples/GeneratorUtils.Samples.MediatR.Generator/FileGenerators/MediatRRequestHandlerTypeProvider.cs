using MediatR;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator.FileGenerators
{
    public sealed class MediatRRequestHandlerTypeProvider : IInputTypeProvider
    {
        public Task<ImmutableArray<TypeInput>> GetInputTypesAsync()
        {
            var requests = typeof(SampleMarkerType)
                .Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => typeof(IBaseRequest).IsAssignableFrom(i)))
                .Select(t => new TypeInput(t, typeof(MediatRRequestHandlerGenerator)))
                .ToImmutableArray();

            return Task.FromResult(requests);
        }
    }
}
