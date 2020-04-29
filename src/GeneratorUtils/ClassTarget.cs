using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GeneratorUtils
{
    public sealed class ClassTarget
    {
        public ClassTarget(
            ISet<ChangeDescriptor> descriptors,
            string targetClassPath,
            string className)
        {
            Descriptors = ImmutableHashSet.Create(descriptors.ToArray());
            TargetClassPath = targetClassPath;
            ClassName = className;
        }

        public IImmutableSet<ChangeDescriptor> Descriptors { get; }
        public string TargetClassPath { get; }
        public string ClassName { get; }
    }
}
