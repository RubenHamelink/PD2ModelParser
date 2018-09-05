namespace PD2ModelParser.Abstract
{
    public interface IFileSystem
    {
        void WriteAllBytes(string path, byte[] bytes);
        byte[] ReadAllBytes(string path);
        void CreateDirectoryIfNotExists(string dir);
    }
}
