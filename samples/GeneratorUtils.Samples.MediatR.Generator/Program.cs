using GeneratorUtils.Samples.MediatR.Generator.FileGenerators;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator
{
    public static class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();

            services.AddGenerator(builder => builder.TargetRootPath = @"D:\GeneratorUtils\samples\GeneratorUtils.Samples.MediatR");

            services.AddTransient<IFileGenerator, MediatRRequestHandlerGenerator>();
            services.AddTransient<IInputTypeProvider, MediatRRequestHandlerTypeProvider>();
            services.AddTransient<IGeneratorService, FileGeneratorService>();

            services.AddLogging(options => options.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger()));

            var sp = services.BuildServiceProvider();

            var generator = sp.GetRequiredService<IGeneratorService>();

            await generator.GenerateFilesAsync();
        }
    }
}
