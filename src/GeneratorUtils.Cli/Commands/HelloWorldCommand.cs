using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;

namespace GeneratorUtils.Cli.Commands
{
    [Command]
    public sealed class HelloWorldCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console) => await console.Output.WriteLineAsync("Hello World!");
    }
}
