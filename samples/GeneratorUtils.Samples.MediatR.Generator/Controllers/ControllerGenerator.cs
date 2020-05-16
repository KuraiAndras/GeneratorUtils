using System;
using System.Threading.Tasks;

namespace GeneratorUtils.Samples.Api.Generator.Controllers
{
    public sealed class ControllerGenerator : MediatRGeneratorBase
    {
        public override Task<FileOutput> CreateFileBodyForFileAsync(Type inputType, string rootDirectory)
        {
            // ReSharper disable once MissingIndent
            const string fileBody =
@"using {3};
using {4};
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace {1}
{
    [ApiController]
    [Route(""api/{5}"")]
    public sealed class {0}Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public {0}Controller(IMediator mediator) => _mediator = mediator;

        [HttpPost(nameof({0}))]
        public async Task<ActionResult<{2}>> Handler([FromBody] {0} request)
        {
            var response = await _mediator.Send(request);

            return new ActionResult<{2}>(response);
        }
    }
}";

            var targetNamespace = GetMediatRTargetNamespace(inputType, "Controllers");

            var path = GetMediatRFilePath(
                targetNamespace,
                "GeneratorUtils.Samples.Api.Controllers.",
                rootDirectory,
                "Controllers",
                inputType.Name + "Controller.cs");

            const string controllersString = "Controllers.";

            var url = targetNamespace
                .Substring(targetNamespace.IndexOf(controllersString, StringComparison.InvariantCulture) + controllersString.Length)
                .Replace(".", "/", StringComparison.InvariantCulture);

            var responseType = GetMediatRResponseType(inputType);

            var tokens = new[]
            {
                inputType.Name,
                targetNamespace,
                responseType.Name,
                inputType.Namespace ?? throw new GeneratorException("No namespace for type"),
                responseType.Namespace ?? throw new GeneratorException("No namespace for type"),
                url,
            };

            return Task.FromResult(new FileOutput(fileBody, path, tokens));
        }
    }
}
