using System.IO;
using PD2ModelParser.Abstract;

namespace PD2ModelParser.Concrete
{
    public class FileSystem : IFileSystem
    {
        public void WriteAllBytes(string path, byte[] bytes)
        {
            File.WriteAllBytes(path, bytes);
        }

        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
        
        public void CreateDirectoryIfNotExists(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
