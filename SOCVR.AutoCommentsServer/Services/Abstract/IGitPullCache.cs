using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    /// <summary>
    /// A service to help rate-limit the times a git repository is updated.
    /// </summary>
    public interface IGitPullCache
    {
        /// <summary>
        /// If true, then enough time has expired where a refresh of the git repo is warented.
        /// </summary>
        /// <returns></returns>
        bool ShouldPullRepository();

        /// <summary>
        /// Forces the next call to ShouldPullRepository() to be true.
        /// </summary>
        void Invalidate();
    }

}
