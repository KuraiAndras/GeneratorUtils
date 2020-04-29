using GeneratorUtils.Cli.Requests.MediatR;
using MediatR;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GeneratorUtils.Tests
{
    public sealed class MediatRTests : IDisposable
    {
        private readonly IMediator _mediator;
        private readonly string _assemblyPath;
        private readonly string _csprojPath;
        private readonly string _handlersPath;

        public MediatRTests(IMediator mediator)
        {
            _mediator = mediator;
            _assemblyPath = @"..\..\..\..\..\samples\GeneratorUtils.Samples.MediatR\bin\Debug\netstandard2.1\GeneratorUtils.Samples.MediatR.dll";
            _csprojPath = @"..\..\..\..\..\samples\GeneratorUtils.Samples.MediatR\GeneratorUtils.Samples.MediatR.csproj";
            _handlersPath = Path.Combine(Path.GetDirectoryName(_csprojPath) ?? throw new DirectoryNotFoundException(_csprojPath), "Handlers");

            BuildTargetProject();
        }

        [Fact]
        public async Task RequestHandlersAreGeneratedFromDll()
        {
            // Arrange
            var request = new GenerateMediatRRequestHandlers { AssemblyPath = _assemblyPath, OutputFolder = Path.Combine(_csprojPath) };

            // Act
            await _mediator.Send(request);

            // Assert
            Assert.Equal(3, GetFilesInFolder());
            BuildTargetProject();
        }

        public void Dispose()
        {
            Directory.Delete(Path.Combine(_assemblyPath, @"..\"), true);
            if (Directory.Exists(_handlersPath)) Directory.Delete(_handlersPath, true);
        }


        private void BuildTargetProject()
        {
            var buildProcess = new Process
            {
                StartInfo = new ProcessStartInfo("dotnet.exe")
                {
                    Arguments = $"build {_csprojPath}",
                    RedirectStandardError = true,
                    UseShellExecute = false,
                }
            };

            buildProcess.Start();
            buildProcess.WaitForExit();

            var exitCode = buildProcess.ExitCode;

            Assert.Equal(0, exitCode);
        }

        private int GetFilesInFolder() => !Directory.Exists(_handlersPath)
            ? 0
            : Directory.EnumerateFiles(_handlersPath, "*.cs", SearchOption.AllDirectories).Count();
    }
}
