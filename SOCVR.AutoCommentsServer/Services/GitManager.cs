using SOCVR.AutoCommentsServer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    /// <summary>
    /// Provides limited access to create an manipulate a local git repository.
    /// </summary>
    public class GitManager : IGitManager
    {
        private readonly IProcessRunner processRunner;
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Creates a new GitManager instance.
        /// </summary>
        /// <param name="processRunnerService">A service which allows for running new processes.</param>
        /// <param name="directoryProviderService">A service which allows for obtaining and manipulating directory in the file system.</param>
        public GitManager(IProcessRunner processRunnerService, IFileSystem fileSystem)
        {
            processRunner = processRunnerService;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Downloads a repository from a remote location into a given directory.
        /// </summary>
        /// <param name="destinationDir">The directory to clone the repo into.</param>
        /// <param name="repoUrl">The url of the remote. Can be in any git-supported format.</param>
        /// <param name="branchName">The name of the branch to checkout once the clone is complete.</param>
        public void Clone(string destinationDir, string repoUrl, string branchName)
        {
            if (!fileSystem.DoesDirectoryExist(destinationDir))
            {
                fileSystem.CreateDirectory(destinationDir);
            }

            var args = $"clone --branch {branchName} {repoUrl} {destinationDir}";

            if (processRunner.Run("git", args) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }

        /// <summary>
        /// Commits all modified files to the repository.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="message">The message to include in the commit.</param>
        public void Commit(string repoDirPath, string message)
        {
            if (processRunner.Run("git", $"commit -a -m \"{message}\"", repoDirPath) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }

        /// <summary>
        /// Creates a new branch based on the current branch.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to create.</param>
        public void CreateBranch(string repoDirPath, string branchName)
        {
            if (processRunner.Run("git", $"branch {branchName}", repoDirPath) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }

        /// <summary>
        /// Returns true if the given path is a valid git repository.
        /// </summary>
        /// <param name="path">A directory to test.</param>
        /// <returns></returns>
        public bool DoesRepositoryExist(string path)
        {
            if (!fileSystem.DoesDirectoryExist(path))
            {
                return false;
            }

            return processRunner.Run("git", $"rev-parse --is-inside-work-tree", path) == 0;
        }

        /// <summary>
        /// Fetches and merges a branch from a remote repo into the current repo.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to update.</param>
        public void Pull(string repoDirPath, string branchName)
        {
            //note, should not need to checkout out a branch. The only reason the repo's branch would be
            //different than the config value is on a dev machine. In production the container will be recreated
            //and the save repo will be thrown away with it.

            if (processRunner.Run("git", $"pull origin {branchName}", repoDirPath) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }

        /// <summary>
        /// Pushes the current branch to the external remote.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        public void Push(string repoDirPath)
        {
            if (processRunner.Run("git", $"push origin HEAD", repoDirPath) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }

        /// <summary>
        /// Changes the repository's current checked out branch.
        /// </summary>
        /// <param name="repoDirPath">The directory of the local repo.</param>
        /// <param name="branchName">The name of the branch to switch to.</param>
        public void SwitchBranch(string repoDirPath, string branchName)
        {
            if (processRunner.Run("git", $"checkout {branchName}", repoDirPath) != 0)
                throw new InvalidOperationException("Git command did not complete successfully");
        }
    }
}
