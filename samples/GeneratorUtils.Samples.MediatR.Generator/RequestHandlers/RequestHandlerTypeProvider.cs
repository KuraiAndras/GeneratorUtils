using MediatR;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator.RequestHandlers
{
    public sealed class RequestHandlerTypeProvider : IInputTypeProvider
    {
        public Task<ImmutableArray<TypeInput>> GetInputTypesAsync()
        {
            var requests = typeof(Startup)
                .Assembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces().Any(i => typeof(IBaseRequest).IsAssignableFrom(i)))
                .Select(t => new TypeInput(t, typeof(RequestHandlerGenerator)))
                .ToImmutableArray();

            return Task.FromResult(requests);
        }
    }
}
