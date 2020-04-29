using System.Collections.Immutable;

namespace GeneratorUtils
{
    public sealed class ClassTarget
    {
        public ClassTarget(
            ChangeDescriptor[] descriptors,
            string targetClassPath,
            string className)
        {
            Descriptors = ImmutableHashSet.Create(descriptors);
            TargetClassPath = targetClassPath;
            ClassName = className;
        }

        public IImmutableSet<ChangeDescriptor> Descriptors { get; }
        public string TargetClassPath { get; }
        public string ClassName { get; }
    }
}
