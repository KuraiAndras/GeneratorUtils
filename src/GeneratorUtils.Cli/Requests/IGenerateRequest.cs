namespace GeneratorUtils.Cli.Requests
{
    public interface IGenerateRequest
    {
        public string AssemblyPath { get; set; }

        public string OutputFolder { get; set; }
    }
}
