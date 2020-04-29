using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.MediatR.Generator
{
    public static class Program
    {
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureServices(s => s.AddGenerator())
                .Build();

            await host.RunAsync();
        }
    }
}
