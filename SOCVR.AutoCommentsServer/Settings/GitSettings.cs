using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Settings
{
    public class GitSettings
    {
        public string Url { get; set; }
        public string Branch { get; set; }
        public string CloneDir { get; set; }
    }
}
