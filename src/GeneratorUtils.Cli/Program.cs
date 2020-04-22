using System.Threading.Tasks;
using CliFx;
using GeneratorUtils.Cli.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratorUtils.Cli
{
    public static class Program
    {
        public static async Task<int> Main()
        {
            var services = new ServiceCollection();

            services.AddCli();

            var serviceProvider = services.BuildServiceProvider();

            return await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(serviceProvider.GetService)
                .Build()
                .RunAsync();
        }
    }
}
