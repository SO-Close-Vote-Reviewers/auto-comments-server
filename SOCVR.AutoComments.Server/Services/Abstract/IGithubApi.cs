using SOCVR.AutoComments.Server.Models;

namespace SOCVR.AutoComments.Server.Services.Abstract;

public interface IGithubApi
{
    Task<IEnumerable<GithubApiContent>> ListDirectoryContents(string path, CancellationToken ct);
    Task<GithubApiContent> GetFileContents(string path, CancellationToken ct);
}
