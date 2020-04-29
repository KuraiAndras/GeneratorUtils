using GeneratorUtils.Samples.MediatR.Generator.Templates;
using MediatR;
using System;

namespace GeneratorUtils.Samples.MediatR.Generator.Generators
{
    public sealed class MediatRWithResponseGenerator : IGenerator<IRequest<object>>
    {
        public ClassTarget CreateChangeDescriptors(Type inputType)
        {
            const string targetPath = "Handlers";
            var className = inputType.Name + "Handler";

            var changes = new[]
            {
                new ChangeDescriptor(typeof(IRequestTemplate), inputType),
                new ChangeDescriptor(typeof(IResponseTemplate), inputType.GetInterface(nameof(IRequest))!.GetGenericArguments()[0]),
            };

            return new ClassTarget(changes, targetPath, className);
        }
    }
}
