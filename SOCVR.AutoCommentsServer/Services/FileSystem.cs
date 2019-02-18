using SOCVR.AutoCommentsServer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    public class FileSystem : IFileSystem
    {
        public void CreateDirectory(string path) => 
            Directory.CreateDirectory(path);

        public bool DoesDirectoryExist(string path) => 
            Directory.Exists(path);

        public IEnumerable<string> EnumerateDirectories(string directoryPath) =>
            Directory.EnumerateDirectories(directoryPath);

        public IEnumerable<string> EnumerateFiles(string directoryPath, string pattern, SearchOption searchOption) =>
            Directory.EnumerateFiles(directoryPath, pattern, searchOption);

        public string ReadAllText(string path) =>
            File.ReadAllText(path);
    }
}
