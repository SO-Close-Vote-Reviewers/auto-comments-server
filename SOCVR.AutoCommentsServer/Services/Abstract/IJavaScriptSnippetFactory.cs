using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    public interface IJavaScriptSnippetFactory
    {
        JavaScriptSnippet GetJavaScriptSnippet();

        /// <summary>
        /// Returns true if the JavaScriptSnippet instance is populated.
        /// </summary>
        bool Exists { get; }
    }
}
