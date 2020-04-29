using System;

namespace GeneratorUtils
{
    public readonly struct ChangeDescriptor
    {
        public ChangeDescriptor(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public Type SourceType { get; }
        public Type TargetType { get; }
    }
}