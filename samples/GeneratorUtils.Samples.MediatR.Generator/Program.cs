﻿using GeneratorUtils.Samples.Api.Generator.Controllers;
using GeneratorUtils.Samples.Api.Generator.RequestHandlers;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator
{
    public static class Program
    {
        public static async Task Main()
        {
            var services = new ServiceCollection();

            Console.WriteLine(Directory.GetCurrentDirectory());

            var targetPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "GeneratorUtils.Samples.Api"));

            services.AddGenerator(builder => builder.TargetRootPath = targetPath);

            services.AddTransient<IFileGenerator, RequestHandlerGenerator>();
            services.AddTransient<IInputTypeProvider, RequestHandlerTypeProvider>();
            services.AddTransient<IFileGenerator, ControllerGenerator>();
            services.AddTransient<IInputTypeProvider, ControllerTypeProvider>();

            services.AddLogging(options => options.AddSerilog(new LoggerConfiguration().WriteTo.Console().CreateLogger()));

            var sp = services.BuildServiceProvider();

            var generator = sp.GetRequiredService<IGeneratorService>();

            await generator.GenerateFilesAsync();
        }
    }
}
