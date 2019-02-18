using Newtonsoft.Json;
using SOCVR.AutoCommentsServer.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services
{
    public class CommentDataTransformer : ICommentDataTransformer
    {
        public string ConvertMarkdownToJsonp(string markdownContent)
        {
            var records = markdownContent.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var proxyData = records
                .Select(x => x.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                .Select(x => new
                {
                    name = Regex.Replace(x[0], "^###(.+)", "$1"),
                    description = x[1]
                });

            var convertedJson = JsonConvert.SerializeObject(proxyData, Formatting.Indented);
            var finalOutput = $"callback({Environment.NewLine}{convertedJson})";
            return finalOutput;
        }
    }
}
