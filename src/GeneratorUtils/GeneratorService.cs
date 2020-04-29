using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class GeneratorService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public GeneratorService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
