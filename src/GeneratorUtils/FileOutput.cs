using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeneratorUtils
{
    public sealed class FileOutput
    {
        public FileOutput(string fileBody, string filePath, IEnumerable<string>? tokens = null)
        {
            FileBody = fileBody;
            FilePath = filePath;
            Tokens = ImmutableArray.Create(tokens.ToArray());
        }

        public string FileBody { get; }
        public string FilePath { get; }
        public ImmutableArray<string> Tokens { get; }
    }
}
