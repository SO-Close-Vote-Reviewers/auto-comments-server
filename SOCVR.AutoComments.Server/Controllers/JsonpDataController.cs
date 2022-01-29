using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SOCVR.AutoComments.Server.Models;
using SOCVR.AutoComments.Server.Services.Abstract;

namespace SOCVR.AutoComments.Server.Controllers
{
    [ApiController]
    [Route("jsonp-data")]
    public class JsonpDataController : ControllerBase
    {
        private readonly IDataStore dataStore;

        public JsonpDataController(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        [HttpGet("{siteName}")]
        public async Task<IActionResult> GetSiteData([FromRoute] string siteName, CancellationToken ct)
        {
            // first, make sure the site name is valid
            var existingSiteNames = await dataStore.ListSiteNames(ct);
            if (!existingSiteNames.Contains(siteName))
            {
                return NotFound();
            }

            var jsonpEntries = new List<JsonpEntry>();
            await foreach (var fileContents in dataStore.ListFileContentsForSite(siteName, ct))
            {
                var lines = fileContents.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); // could be either or both
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("###"))
                    {
                        jsonpEntries.Add(new JsonpEntry
                        {
                            Name = lines[i][3..], // take off the first 3 chars (so the "###")
                            Description = lines[i + 1]
                        });
                    }
                }
            }

            var finalJsonpString = "callback(" + Environment.NewLine + JsonConvert.SerializeObject(jsonpEntries, Formatting.Indented) + ")";
            return Content(finalJsonpString);
        }
    }
}
