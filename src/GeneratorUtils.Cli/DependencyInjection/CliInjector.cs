using System.Reflection;
using AutoMapper;
using FluentValidation;
using GeneratorUtils.Cli.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace GeneratorUtils.Cli.DependencyInjection
{
    public static class CliInjector
    {
        private static readonly Assembly[] AssembliesToScan = { typeof(CliInjector).Assembly, };

        public static IServiceCollection AddCli(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));

            services.AddMediatR(AssembliesToScan);
            services.AddAutoMapper(AssembliesToScan);
            services.AddValidatorsFromAssemblies(AssembliesToScan);

            services.AddTransient<HelloWorldCommand>();

            return services;
        }
    }
}
