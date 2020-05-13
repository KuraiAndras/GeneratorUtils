using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeneratorUtils
{
    public sealed class FileOutput
    {
        public FileOutput(
            string fileBody,
            string filePath,
            IEnumerable<string> tokens,
            Type? tokenizer = null,
            StringComparison stringComparison = StringComparison.InvariantCulture,
            bool replaceIfExists = true)
        {
            FileBody = fileBody;
            FilePath = filePath;
            ReplaceIfExists = replaceIfExists;
            Tokenizer = tokenizer ?? typeof(DefaultTokenizer);
            Tokens = ImmutableArray.Create(tokens.ToArray());
            StringComparison = stringComparison;
        }

        public string FileBody { get; }
        public string FilePath { get; }
        public bool ReplaceIfExists { get; }
        public Type Tokenizer { get; }
        public ImmutableArray<string> Tokens { get; }
        public StringComparison StringComparison { get; }
    }
}
