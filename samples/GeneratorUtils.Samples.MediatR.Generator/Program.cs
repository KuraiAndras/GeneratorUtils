using GeneratorUtils.Samples.MediatR.Generator.FileGenerators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator
{
    public static class Program
    {
        public static async Task Main()
        {
            var assembliesToScan = new[]
            {
                typeof(Program).Assembly,
                typeof(Injector).Assembly,
            };

            var host = new HostBuilder()
                .ConfigureServices(s => s
                    .AddGenerator(builder => builder.TargetRootPath = @"D:\GeneratorUtils\samples\GeneratorUtils.Samples.MediatR")
                    .AddTransient<IFileGenerator, MediatRRequestHandlerGenerator>()
                    .AddTransient<IInputTypeProvider, MediatRRequestHandlerTypeProvider>()
                    .AddMediatR(assembliesToScan))
                .UseSerilog((_, config) => config.WriteTo.Console())
                .Build();

            await host.RunAsync();
        }
    }
}
