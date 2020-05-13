using System;
using System.Runtime.Serialization;

namespace GeneratorUtils
{
    [Serializable]
    public class GeneratorException : Exception
    {
        public GeneratorException()
        {
        }

        public GeneratorException(string message)
            : base(message)
        {
        }

        public GeneratorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected GeneratorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
