using System;

namespace GeneratorUtils
{
    public interface IGenerator<T>
    {
        public ClassTarget CreateChangeDescriptors(Type inputType);
    }
}
