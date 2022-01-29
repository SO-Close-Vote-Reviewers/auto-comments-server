namespace SOCVR.AutoComments.Server.Services.Abstract
{
    // This service also deals with the cache
    public interface IDataStore
    {
        Task<IEnumerable<string>> ListSiteNames(CancellationToken ct);
        IAsyncEnumerable<string> ListFileContentsForSite(string site, CancellationToken ct);
    }
}
