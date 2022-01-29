using Microsoft.AspNetCore.Mvc.RazorPages;
using SOCVR.AutoComments.Server.Services.Abstract;

namespace SOCVR.AutoComments.Server.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataStore dataStore;

        public IEnumerable<string> SiteNames { get; set; } = Array.Empty<string>();

        public IndexModel(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public async Task OnGetAsync(CancellationToken ct)
        {
            SiteNames = await dataStore.ListSiteNames(ct);
        }
    }
}
