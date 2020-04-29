using System;

namespace GeneratorUtils
{
    public sealed class TypeInput
    {
        public TypeInput(Type targetType, Type handlerType)
        {
            TargetType = targetType;
            HandlerType = handlerType;
        }

        public Type TargetType { get; }
        public Type HandlerType { get; }
    }
}