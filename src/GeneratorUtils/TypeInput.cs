using System;

namespace GeneratorUtils
{
    public sealed class TypeInput
    {
        public TypeInput(Type inputType, Type handlerType)
        {
            InputType = inputType;
            HandlerType = handlerType;
        }

        public Type InputType { get; }

        public Type HandlerType { get; }
    }
}
