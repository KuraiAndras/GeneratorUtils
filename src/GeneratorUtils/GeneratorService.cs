using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class GeneratorService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
