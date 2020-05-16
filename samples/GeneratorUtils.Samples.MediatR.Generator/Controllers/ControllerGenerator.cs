using System;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator.Controllers
{
    public sealed class ControllerGenerator : IFileGenerator
    {
        public Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory)
        {
            throw new NotImplementedException();
        }
    }
}
