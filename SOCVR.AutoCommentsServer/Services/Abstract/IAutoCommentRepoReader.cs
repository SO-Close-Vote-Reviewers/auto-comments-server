using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    public interface IAutoCommentRepoReader
    {
        /// <summary>
        /// Returns a list of the different sites that have auto comments.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSites();

        /// <summary>
        /// Searches for all markdown files in the given site and returns the concatinated data.
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        string GetCombinedMarkdownFileContent(string siteName);
    }
}
