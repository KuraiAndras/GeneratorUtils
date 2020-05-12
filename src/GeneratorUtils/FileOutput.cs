namespace GeneratorUtils
{
    public sealed class FileOutput
    {
        public FileOutput(string fileBody, string filePath)
        {
            FileBody = fileBody;
            FilePath = filePath;
        }

        public string FileBody { get; }
        public string FilePath { get; }
    }
}
