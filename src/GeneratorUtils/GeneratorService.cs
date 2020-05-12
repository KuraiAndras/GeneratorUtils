using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneratorUtils
{
    public sealed class GeneratorService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public GeneratorService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var typeInputs = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IInputHandler).IsAssignableFrom(t))
                .Select(t => (IInputHandler)_serviceProvider.GetService(t))
                .Select(h => h.CreateInput())
                .SelectMany(i => i.TypeInputs);


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
