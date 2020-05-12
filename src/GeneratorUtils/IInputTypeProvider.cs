using System.Collections.Immutable;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public interface IInputTypeProvider
    {
        Task<ImmutableArray<TypeInput>> GetInputTypesAsync();
    }
}
