using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    /// <summary>
    /// Provides limited access to create an manipulate a local git repository.
    /// </summary>
    public interface IGitManager
    {
        /// <summary>
        /// Downloads a repository from a remote location into a given directory.
        /// </summary>
        /// <param name="destinationDir">The directory to clone the repo into.</param>
        /// <param name="repoUrl">The url of the remote. Can be in any git-supported format.</param>
        /// <param name="branchName">The name of the branch to checkout once the clone is complete.</param>
        void Clone(string destinationDir, string repoUrl, string branchName);

        /// <summary>
        /// Fetches and merges a branch from a remote repo into the current repo.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to update.</param>
        void Pull(string repoDirPath, string branchName);

        /// <summary>
        /// Returns true if the given path is a valid git repository.
        /// </summary>
        /// <param name="path">A directory to test.</param>
        /// <returns></returns>
        bool DoesRepositoryExist(string path);

        /// <summary>
        /// Pushes the current branch to the external remote.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        void Push(string repoDirPath);

        /// <summary>
        /// Changes the repository's current checked out branch.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to switch to.</param>
        void SwitchBranch(string repoDirPath, string branchName);

        /// <summary>
        /// Creates a new branch based on the current branch.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to create.</param>
        void CreateBranch(string repoDirPath, string branchName);

        /// <summary>
        /// Commits all modified files to the repository.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="message">The message to include in the commit.</param>
        void Commit(string repoDirPath, string message);

    }
}
