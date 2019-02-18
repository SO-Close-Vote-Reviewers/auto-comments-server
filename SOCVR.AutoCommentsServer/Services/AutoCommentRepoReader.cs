using Microsoft.Extensions.Options;
using SOCVR.AutoCommentsServer.Services.Abstract;
using SOCVR.AutoCommentsServer.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    public class AutoCommentRepoReader : IAutoCommentRepoReader
    {
        private readonly GitSettings settings;
        private readonly IFileSystem fileSystem;

        public AutoCommentRepoReader(IOptions<GitSettings> gitSettings, IFileSystem fileSystem)
        {
            settings = gitSettings.Value;
            this.fileSystem = fileSystem;
        }

        public string GetCombinedMarkdownFileContent(string siteName)
        {
            var siteDirPath = Path.Combine(settings.CloneDir, "sites", siteName);
            var siteDir = new DirectoryInfo(siteDirPath);

            var markdownFilePaths = fileSystem.EnumerateFiles(siteDirPath, "*.md", SearchOption.AllDirectories);

            var fileContents = markdownFilePaths
                .OrderBy(x => x)
                .Select(x => fileSystem.ReadAllText(x));

            var combinedContents = string.Join(Environment.NewLine, fileContents);

            return combinedContents;
        }

        public IEnumerable<string> GetSites()
        {
            var sitesDirPath = Path.Combine(settings.CloneDir, "sites");
            return fileSystem.EnumerateDirectories(sitesDirPath).Select(x => Path.GetFileName(x));
        }
    }
}
