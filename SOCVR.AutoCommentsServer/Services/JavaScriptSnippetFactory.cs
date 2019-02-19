using Microsoft.ApplicationInsights.AspNetCore;
using SOCVR.AutoCommentsServer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    public class JavaScriptSnippetFactory : IJavaScriptSnippetFactory
    {
        private readonly JavaScriptSnippet snippet;

        public JavaScriptSnippetFactory(JavaScriptSnippet snippet = null)
        {
            this.snippet = snippet;
        }

        public bool Exists => snippet != null;

        public JavaScriptSnippet GetJavaScriptSnippet() => snippet;
    }
}
