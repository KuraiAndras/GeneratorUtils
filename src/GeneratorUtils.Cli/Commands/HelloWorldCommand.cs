using CliFx;
using CliFx.Attributes;
using System;
using System.Threading.Tasks;

namespace GeneratorUtils.Cli.Commands
{
    [Command("ColorMessage")]
    public sealed class HelloWorldCommand : ICommand
    {
        [CommandParameter(0, Description = "Display text", Name = nameof(DisplayText))]
        public string DisplayText { get; set; } = string.Empty;

        [CommandOption(nameof(TextColor), 't', Description = "Foreground Color", IsRequired = false)]
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;

        [CommandOption(nameof(BackgroundColor), 'b', Description = "Foreground Color", IsRequired = false)]
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var oldBackground = console.BackgroundColor;

            console.ForegroundColor = TextColor;
            console.BackgroundColor = BackgroundColor;

            await console.Output.WriteLineAsync(DisplayText);

            console.BackgroundColor = oldBackground;
        }
    }
}
