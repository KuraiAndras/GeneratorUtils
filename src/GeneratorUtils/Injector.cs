using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeneratorUtils
{
    public static class Injector
    {
        public static IServiceCollection AddGenerator(this IServiceCollection services, Action<GeneratorOptions>? optionsBuilder = default)
        {
            var options = new GeneratorOptions();
            optionsBuilder?.Invoke(options);

            services.AddSingleton<IGeneratorOptions, GeneratorOptions>();

            return services;
        }
    }
}
