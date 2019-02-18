using Microsoft.Extensions.Options;
using SOCVR.AutoCommentsServer.Services.Abstract;
using SOCVR.AutoCommentsServer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    public class GitInstanceManager : IGitInstanceManager
    {
        private readonly IGitManager git;
        private readonly IGitPullCache gitCache;
        private readonly GitSettings gitSettings;

        public GitInstanceManager(IGitManager git, IGitPullCache gitCache, IOptions<GitSettings> gitSettings)
        {
            this.git = git;
            this.gitCache = gitCache;
            this.gitSettings = gitSettings.Value;
        }

        public void UpdateOrCreateInstance()
        {
            if (!git.DoesRepositoryExist(gitSettings.CloneDir))
            {
                git.Clone(gitSettings.CloneDir, gitSettings.Url, gitSettings.Branch);
            }

            if (gitCache.ShouldPullRepository())
            {
                try
                {
                    git.Pull(gitSettings.CloneDir, gitSettings.Branch);
                }
                catch
                {
                    gitCache.Invalidate();
                    throw;
                }
            }
        }
    }
}
