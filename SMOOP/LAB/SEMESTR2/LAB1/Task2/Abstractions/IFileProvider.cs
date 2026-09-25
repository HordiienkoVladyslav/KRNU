namespace LAB1
{
    public interface IFileProvider
    {
        bool Exists(string path);
        List<string> ReadFileList(string manifestPath);
        string ReadText(string path);
        void EnsureDemoFilesExist(string manifestPath);
    }
}