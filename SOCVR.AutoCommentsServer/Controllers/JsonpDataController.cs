using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOCVR.AutoCommentsServer.Services.Abstract;

namespace SOCVR.AutoCommentsServer.Controllers
{
    [Route("jsonp-data")]
    public class JsonpDataController : Controller
    {
        private readonly IAutoCommentRepoReader repoReader;
        private readonly IGitInstanceManager gitInstanceManager;
        private readonly ICommentDataTransformer transformer;

        public JsonpDataController(IAutoCommentRepoReader repoReader, IGitInstanceManager gitInstanceManager, ICommentDataTransformer transformer)
        {
            this.repoReader = repoReader;
            this.gitInstanceManager = gitInstanceManager;
            this.transformer = transformer;
        }

        [HttpGet("{siteName}")]
        public IActionResult GetSiteData([FromRoute] string siteName)
        {
            gitInstanceManager.UpdateOrCreateInstance();

            var combinedMarkdown = repoReader.GetCombinedMarkdownFileContent(siteName);
            var jsonp = transformer.ConvertMarkdownToJsonp(combinedMarkdown);

            return Content(jsonp);
        }
    }
}
