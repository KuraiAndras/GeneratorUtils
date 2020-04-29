using System.Collections.Immutable;

namespace GeneratorUtils
{
    public readonly struct ClassTarget
    {
        public ClassTarget(IImmutableSet<ChangeDescriptor> descriptors, string targetClassPath)
        {
            Descriptors = descriptors;
            TargetClassPath = targetClassPath;
        }

        public IImmutableSet<ChangeDescriptor> Descriptors { get; }
        public string TargetClassPath { get; }
    }
}