using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SOCVR.AutoCommentsServer.Services.Abstract;
using SOCVR.AutoCommentsServer.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    /// <summary>
    /// A service to help rate-limit the times a git repository is updated.
    /// </summary>
    public class GitPullCache : IGitPullCache
    {
        private readonly IMemoryCache cache;
        private readonly GitPullCacheSettings settings;
        private const string CacheKey = "ShouldPullFromGitRepo";

        /// <summary>
        /// Creates a new GitPullCache instance.
        /// </summary>
        /// <param name="settings">Settings to use when determining to allow updating or not.</param>
        /// <param name="memoryCache">A memory cache instance.</param>
        public GitPullCache(IOptions<GitPullCacheSettings> settings, IMemoryCache memoryCache)
        {
            cache = memoryCache;
            this.settings = settings.Value;
        }

        /// <summary>
        /// If true, then enough time has expired where a refresh of the git repo is warented.
        /// </summary>
        /// <returns></returns>
        public bool ShouldPullRepository()
        {
            if (!cache.TryGetValue(CacheKey, out Guid cacheValue))
            {
                //cache value does not exist, set the timer and return true;
                cacheValue = Guid.NewGuid();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(settings.CacheHoldTimeSeconds));

                cache.Set(CacheKey, cacheValue, cacheEntryOptions);

                return true;
            }

            // there was a cache value
            return false;
        }

        /// <summary>
        /// Forces the next call to ShouldPullRepository() to be true.
        /// </summary>
        public void Invalidate()
        {
            cache.Remove(CacheKey);
        }
    }
}
