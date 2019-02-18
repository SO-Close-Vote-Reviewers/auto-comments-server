using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    public interface ICommentDataTransformer
    {
        string ConvertMarkdownToJsonp(string markdownContent);
    }
}
