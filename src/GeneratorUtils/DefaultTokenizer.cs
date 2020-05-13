using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class DefaultTokenizer : ITokenizer
    {
        public Task<string> TokenizeAsync(string fileBody, ImmutableArray<string> tokens, StringComparison stringComparison)
        {
            var outputString = fileBody;
            for (var i = 0; i < tokens.Length; i++)
            {
                outputString = outputString.Replace("{" + i + "}", tokens[i], stringComparison);
            }

            return Task.FromResult(outputString);
        }
    }
}
