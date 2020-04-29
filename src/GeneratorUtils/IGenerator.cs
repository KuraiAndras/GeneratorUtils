using System;

namespace GeneratorUtils
{
    public interface IGenerator
    {
        public ClassTarget CreateChangeDescriptors(Type inputType);
    }
}
