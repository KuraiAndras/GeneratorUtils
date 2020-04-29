using System;

namespace GeneratorUtils
{
    public sealed class ChangeDescriptor : IEquatable<ChangeDescriptor>
    {
        public ChangeDescriptor(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public Type SourceType { get; }
        public Type TargetType { get; }

        public bool Equals(ChangeDescriptor? other) =>
            !ReferenceEquals(null, other)
            && (ReferenceEquals(this, other)
                || SourceType == other.SourceType
                && TargetType == other.TargetType);

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || obj is ChangeDescriptor other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(SourceType, TargetType);
    }
}
