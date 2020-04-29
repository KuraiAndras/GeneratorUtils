using System;

namespace GeneratorUtils
{
    public interface IGenerator<TInputType> where TInputType : Type
    {
        public ClassTarget CreateChangeDescriptors(TInputType inputType);
    }
}
