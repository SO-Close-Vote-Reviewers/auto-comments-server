using SOCVR.AutoCommentsServer.Services.Abstract;
using System.Diagnostics;

namespace SOCVR.AutoCommentsServer.Services
{
    /// <summary>
    /// Facilitates the launching of a process.
    /// </summary>
    public class ProcessRunner : IProcessRunner
    {
        /// <summary>
        /// Runs a process. Returns the exit code.
        /// </summary>
        /// <param name="program">The name or path to the program to run.</param>
        /// <param name="arguments">Command line arguments to pass to the program.</param>
        /// <returns></returns>
        public int Run(string program, string arguments)
        {
            return Run(program, arguments, null);
        }

        /// <summary>
        /// Runs a process. Returns the exit code.
        /// </summary>
        /// <param name="program">The name or path to the program to run.</param>
        /// <param name="arguments">Command line arguments to pass to the program.</param>
        /// <param name="workingDirectory">The directory to invoke the command from.</param>
        /// <returns></returns>
        public int Run(string program, string arguments, string workingDirectory)
        {
            var info = new ProcessStartInfo(program)
            {
                Arguments = arguments
            };

            if (!string.IsNullOrWhiteSpace(workingDirectory))
            {
                info.WorkingDirectory = workingDirectory;
            }

            var process = System.Diagnostics.Process.Start(info);
            process.WaitForExit();

            return process.ExitCode;
        }
    }
}
