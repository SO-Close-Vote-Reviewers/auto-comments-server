using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    public interface IFileSystem
    {
        IEnumerable<string> EnumerateFiles(string directoryPath, string pattern, SearchOption searchOption);

        string ReadAllText(string path);

        /// <summary>
        /// Returns an enumerable collection of directory information in the current directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        IEnumerable<string> EnumerateDirectories(string directoryPath);
    }
}
