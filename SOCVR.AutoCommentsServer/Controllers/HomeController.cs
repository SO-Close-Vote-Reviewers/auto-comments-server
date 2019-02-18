using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOCVR.AutoCommentsServer.Models;
using SOCVR.AutoCommentsServer.Services.Abstract;

namespace SOCVR.AutoCommentsServer.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IGitInstanceManager gitInstanceManager;
        private readonly IAutoCommentRepoReader repoReader;

        public HomeController(IGitInstanceManager gitInstanceManager, IAutoCommentRepoReader repoReader)
        {
            this.gitInstanceManager = gitInstanceManager;
            this.repoReader = repoReader;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            gitInstanceManager.UpdateOrCreateInstance();

            var sites = repoReader.GetSites();
            return View(sites);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
