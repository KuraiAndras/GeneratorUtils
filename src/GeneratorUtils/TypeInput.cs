using System;

namespace GeneratorUtils
{
    public sealed class TypeInput
    {
        public TypeInput(Type inputType, Type generatorType)
        {
            InputType = inputType;
            GeneratorType = generatorType;
        }

        public Type InputType { get; }

        public Type GeneratorType { get; }
    }
}
