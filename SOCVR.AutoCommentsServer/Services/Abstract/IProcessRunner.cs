using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOCVR.AutoCommentsServer.Services.Abstract
{
    /// <summary>
    /// Facilitates the launching of a process.
    /// </summary>
    public interface IProcessRunner
    {
        /// <summary>
        /// Runs a process. Returns the exit code.
        /// </summary>
        /// <param name="program">The name or path to the program to run.</param>
        /// <param name="arguments">Command line arguments to pass to the program.</param>
        /// <returns></returns>
        int Run(string program, string arguments);

        /// <summary>
        /// Runs a process. Returns the exit code.
        /// </summary>
        /// <param name="program">The name or path to the program to run.</param>
        /// <param name="arguments">Command line arguments to pass to the program.</param>
        /// <param name="workingDirectory">The directory to invoke the command from.</param>
        /// <returns></returns>
        int Run(string program, string arguments, string workingDirectory);
    }
}
