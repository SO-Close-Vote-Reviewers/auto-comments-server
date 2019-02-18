using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Settings
{
    /// <summary>
    /// Settings to use with the GitPullCache service.
    /// </summary>
    public class GitPullCacheSettings
    {
        /// <summary>
        /// The number of seconds to rate limit git pulls.
        /// </summary>
        public int CacheHoldTimeSeconds { get; set; }
    }
}
