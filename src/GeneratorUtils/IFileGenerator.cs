using System;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public interface IFileGenerator
    {
        Task<FileOutput> CreateFileBodyForFileAsync(Type type, string rootDirectory);
    }
}
