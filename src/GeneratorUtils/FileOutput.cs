using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeneratorUtils
{
    public sealed class FileOutput
    {
        public FileOutput(string fileBody, string filePath, IEnumerable<string> tokens, Type? tokenizer = null)
        {
            FileBody = fileBody;
            FilePath = filePath;
            Tokenizer = tokenizer ?? typeof(DefaultTokenizer);
            Tokens = ImmutableArray.Create(tokens.ToArray());
        }

        public string FileBody { get; }
        public string FilePath { get; }
        public Type Tokenizer { get; }
        public ImmutableArray<string> Tokens { get; }
        public StringComparison StringComparison { get; set; } = StringComparison.InvariantCulture;
    }
}
