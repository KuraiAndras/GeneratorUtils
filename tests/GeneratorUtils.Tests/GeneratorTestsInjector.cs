using GeneratorUtils.Cli.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratorUtils.Tests
{
    public static class GeneratorTestsInjector
    {
        public static IServiceCollection AddTests(this IServiceCollection services)
        {
            services.AddCli();

            return services;
        }
    }
}
