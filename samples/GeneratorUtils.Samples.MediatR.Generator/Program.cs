using MediatR;
using Microsoft.Extensions.Hosting;
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
                    .AddGenerator()
                    .AddMediatR(assembliesToScan))
                .Build();

            await host.RunAsync();
        }
    }
}
