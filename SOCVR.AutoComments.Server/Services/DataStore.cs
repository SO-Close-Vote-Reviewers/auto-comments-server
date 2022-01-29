using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SOCVR.AutoComments.Server.Services.Abstract;
using System.Runtime.CompilerServices;
using System.Text;

namespace SOCVR.AutoComments.Server.Services
{
    public class DataStore : IDataStore
    {
        private readonly IMemoryCache cache;
        private readonly IGithubApi githubApi;
        private readonly Options options;

        private const string SitesDirName = "sites";

        public DataStore(IMemoryCache cache, IGithubApi githubApi, IOptions<Options> options)
        {
            this.cache = cache;
            this.githubApi = githubApi;
            this.options = options.Value;
        }

        public async IAsyncEnumerable<string> ListFileContentsForSite(string site, [EnumeratorCancellation] CancellationToken ct)
        {
            var sitePath = SitesDirName + "/" + site;
            await foreach (var fileContent in ListDirectoryFileContents(sitePath, ct))
            {
                yield return fileContent;
            }
        }

        private async IAsyncEnumerable<string> ListDirectoryFileContents(string path, [EnumeratorCancellation] CancellationToken ct)
        {
            var topDirContents = await cache.GetOrCreate(path, async ce =>
            {
                ce.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(options.CacheExpirationSeconds);
                return await githubApi.ListDirectoryContents(path, ct);
            });

            var files = topDirContents.Where(x => x.Type == "file");
            foreach (var file in files)
            {
                var fileContentsData = await cache.GetOrCreateAsync(file.Path, async ce =>
                {
                    ce.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(options.CacheExpirationSeconds);
                    return await githubApi.GetFileContents(file.Path, ct);
                });

                var decodedFileContents = Encoding.UTF8.GetString(Convert.FromBase64String(fileContentsData.Content ?? ""));
                yield return decodedFileContents;
            }

            var subDirectories = topDirContents.Where(x => x.Type == "dir");
            foreach (var subDirectory in subDirectories)
            {
                await foreach (var subDirFile in ListDirectoryFileContents(subDirectory.Path, ct))
                {
                    yield return subDirFile;
                }
            }
        }

        public async Task<IEnumerable<string>> ListSiteNames(CancellationToken ct)
        {
            return await cache.GetOrCreateAsync(SitesDirName, async ce =>
            {
                ce.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(options.CacheExpirationSeconds);
                var data = await githubApi.ListDirectoryContents(SitesDirName, ct);
                return data.Select(x => x.Name);
            });
        }

        public class Options
        {
            public long CacheExpirationSeconds { get; set; } = 60 * 5_000; // default 5 minutes
        }
    }
}
