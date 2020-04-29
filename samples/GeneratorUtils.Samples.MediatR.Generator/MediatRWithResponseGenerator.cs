using GeneratorUtils.Samples.MediatR.Generator.Templates;
using GeneratorUtils.Samples.MediatR.Requests.Orders;
using MediatR;
using System;
using System.Linq;

namespace GeneratorUtils.Samples.MediatR.Generator
{
    public sealed class MediatRWithResponseGenerator : IGenerator, IInputHandler
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

            return new ClassTarget(changes.ToHashSet(), targetPath, className);
        }

        public GeneratorInput CreateInput()
        {
            var inputTypes = typeof(AddOrder)
                .Assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IRequest<object>).IsAssignableFrom(t))
                .Select(t => new TypeInput(t, typeof(MediatRWithResponseGenerator)));

            return new GeneratorInput(inputTypes.ToHashSet());
        }
    }
}
