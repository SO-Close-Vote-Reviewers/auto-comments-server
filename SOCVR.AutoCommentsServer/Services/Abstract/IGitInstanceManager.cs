using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    public interface IGitInstanceManager
    {
        /// <summary>
        /// Updates the git repository in the configured location.
        /// Will clone the repo if it does not exist.
        /// </summary>
        void UpdateOrCreateInstance();
    }
}
