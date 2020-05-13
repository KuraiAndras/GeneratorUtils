using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public interface ITokenizer
    {
        Task<string> TokenizeAsync(string fileBody, ImmutableArray<string> tokens, StringComparison stringComparison);
    }
}
