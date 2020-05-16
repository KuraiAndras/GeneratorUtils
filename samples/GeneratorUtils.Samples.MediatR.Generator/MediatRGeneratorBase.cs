using MediatR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator
{
    public abstract class MediatRGeneratorBase : IFileGenerator
    {
        public abstract Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory);

        protected static Type GetMediatRResponseType(Type inputType)
        {
            Type? responseType = null;
            foreach (var inputTypeInterface in inputType.GetInterfaces())
            {
                if (inputTypeInterface.IsGenericType && inputTypeInterface.GetGenericTypeDefinition() == typeof(IRequest<>))
                {
                    responseType = inputTypeInterface.GetGenericArguments()[0];
                    break;
                }

                if (inputTypeInterface == typeof(IRequest))
                {
                    responseType = typeof(Unit);
                    break;
                }
            }

            if (responseType is null) throw new GeneratorException("No response type");

            return responseType;
        }

        protected static string GetMediatRTargetNamespace(Type inputType, string namespaceUniqueRoot) =>
            inputType.Namespace?.Replace("Requests", namespaceUniqueRoot, StringComparison.InvariantCulture) ?? throw new GeneratorException("No namespace");

        protected static string GetMediatRFilePath(string targetNamespace, string baseRawNamespace, string rootDirectory, string rootUniqueNamespace, string classFileName)
        {
            var relativePath = targetNamespace
                .Replace(baseRawNamespace, string.Empty, StringComparison.InvariantCulture)
                .Replace(".", "\\", StringComparison.InvariantCulture);

            return Path.GetFullPath(Path.Combine(rootDirectory, rootUniqueNamespace, relativePath, classFileName));
        }
    }
}
