using MediatR;

namespace GeneratorUtils.Cli.Requests.MediatR
{
    public sealed class GenerateMediatRRequestHandlers : IRequest, IGenerateRequest
    {
        public string AssemblyPath { get; set; } = string.Empty;

        public string OutputFolder { get; set; } = string.Empty;
    }
}
