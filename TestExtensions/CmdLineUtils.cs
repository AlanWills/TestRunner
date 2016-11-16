// If you need to debug the output/error of a process, change this to HELP and it will dump
// the process output into a file called Output.txt and the process error into a file called Error.txt
// in the output directory for the tests you are running
#define _HELP

using System.Diagnostics;

#if HELP
using System.IO;
#endif

namespace TestExtensions
{
    /// <summary>
    /// Utility class for performing git operations from within C#
    /// </summary>
    public static class CmdLineUtils
    {
        private static string MSBuild = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe";

        /// <summary>
        /// Creates the process start info for running a command in a windowless process in the current working directory with the inputted arguments.
        /// StdError and StdOutput are redirected since it is windowless.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static ProcessStartInfo CreateCmdLineProcessStartInfo(string arguments)
        {
            ProcessStartInfo cmdInfo = new ProcessStartInfo();
            cmdInfo.CreateNoWindow = true;
            cmdInfo.UseShellExecute = false;
            cmdInfo.WindowStyle = ProcessWindowStyle.Maximized;
            cmdInfo.Arguments = arguments;

#if HELP
            cmdInfo.RedirectStandardOutput = true;
            cmdInfo.RedirectStandardError = true;
#endif

            return cmdInfo;
        }

        /// <summary>
        /// Creates a process which builds the inputted project using MSBuild.
        /// An output of 0 indicates success.  Anything else is failure.</summary>
        /// <param name="projectDirectory">The directory the project we are building is in</param>
        /// <param name="projectName">The full name of the project to build e.g. MyTestProject.csproj</param>
        /// <param name="configuration">The build configuration e.g. Debug, Release, Release Demo</param>
        /// <param name="platform">The platform to target, e.g. x86, x64, AnyCPU</param>
        /// <returns>0 if the build was a success.  -1 is a timeout.  Any other result is a failure in MSBuild.</returns>
        public static int RunMSBuild(string projectDirectory, string projectName, string configuration, string platform)
        {
            using (Process process = new Process())
            {
                process.StartInfo = CreateCmdLineProcessStartInfo(projectName + " /m /tv:14.0 /t:Build /p:Configuration=" + configuration + " /p:Platform=" + platform);
                process.StartInfo.FileName = MSBuild;
                process.StartInfo.WorkingDirectory = projectDirectory;

                process.Start();

                // Wait five minutes and then timeout
                bool result = process.WaitForExit(300000);

#if HELP
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Output.txt"), process.StandardOutput.ReadToEnd());
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Error.txt"), process.StandardError.ReadToEnd());
#endif

                // If we have timed out, return a failure value
                return result ? process.ExitCode : -1;
            }
        }
    }
}
