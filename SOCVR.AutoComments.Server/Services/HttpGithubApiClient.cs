using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using SOCVR.AutoComments.Server.Models;
using SOCVR.AutoComments.Server.Services.Abstract;

namespace SOCVR.AutoComments.Server.Services;

public class HttpGithubApiClient : IGithubApi
{
    private const string ApiBaseUrl = "https://api.github.com";
    private const string AcceptHeader = "vnd.github.v3+json";
    private const string UserAgentHeader = "SOCVRAutoCommentsServer";

    private readonly Options options;
    private readonly IFlurlClient flurlClient;

    public HttpGithubApiClient(IOptions<Options> options)
    {
        this.options = options.Value;

        flurlClient = new FlurlClient()
            .WithHeader("Accept", AcceptHeader)
            .WithHeader("User-Agent", UserAgentHeader);

        if (this.options.ApiKey is not null)
        {
            flurlClient = flurlClient.WithBasicAuth("", this.options.ApiKey);
        }
    }

    public async Task<GithubApiContent> GetFileContents(string path, CancellationToken ct)
    {
        var url = MakeContentUrl(path);

        return await url
            .WithClient(flurlClient)
            .GetJsonAsync<GithubApiContent>(ct);
    }

    public async Task<IEnumerable<GithubApiContent>> ListDirectoryContents(string path, CancellationToken ct)
    {
        var url = MakeContentUrl(path);

        return await url
            .WithClient(flurlClient)
            .GetJsonAsync<List<GithubApiContent>>(ct);
    }

    private Url MakeContentUrl(string path) => ApiBaseUrl
        .AppendPathSegment("repos")
        .AppendPathSegment("SO-Close-Vote-Reviewers")
        .AppendPathSegment("auto-comments")
        .AppendPathSegment("contents")
        .AppendPathSegment(path)
        .SetQueryParam("ref", options.Branch);

    public class Options
    {
        /// <summary>
        /// A Github personal access token. It can be made by any user, and it doesn't need any scopes - it's read only info to public data.
        /// </summary>
        public string? ApiKey { get; set; }
        public string Branch { get; set; } = "master";
    }
}
