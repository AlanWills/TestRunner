using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using TestRunner.Extensions;

namespace TestRunner
{
    // Extension of the process class which reroutes output and povides extra custom functionality for running tests
    // Also wraps up all of the process logic for starting, reading output/error and writing to the output files
    // It should only be created from the TestRunnerProcessManager class
    public class TestRunnerProcess : Process
    {
        #region Properties and Fields

        /// <summary>
        /// The timer which will ensure the process repeats itself based on the value we gave it
        /// </summary>
        private Timer Timer { get; set; }

        public string Name { get; private set; }
        
        private Project Project { get; set; }

        #endregion

        public TestRunnerProcess(Project projectToTest)
        {
            Project = projectToTest;

            Name = Project.Name;
            EnableRaisingEvents = true;

            StartInfo = CreateCmdLineProcessStartInfo(Path.GetDirectoryName(Project.FullPathToDll), Path.GetFileName(Project.FullPathToDll));

            Timer = new Timer(RerunTestProcess, 0, TimeSpan.FromMilliseconds(0), Project.Frequency.ToTimeSpan());
        }

        private void RerunTestProcess(object state)
        {
            bool startResult = Start();
            Debug.Assert(startResult);
        }

        /// <summary>
        /// Creates the process start info for running the test command in a windowless process in the inputted working directory with the inputted arguments.
        /// StdError and StdOutput are redirected since it is windowless.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private ProcessStartInfo CreateCmdLineProcessStartInfo(string workingDirectory, string dllName)
        {
            ProcessStartInfo cmdInfo = new ProcessStartInfo();
            cmdInfo.CreateNoWindow = true;
            cmdInfo.UseShellExecute = false;
            cmdInfo.Arguments = dllName + " /inIsolation /Platform:" + Project.Platform.ToString() + " /Logger:trx";
            cmdInfo.WorkingDirectory = workingDirectory;
            cmdInfo.FileName = Settings.VSTestPath;

            return cmdInfo;
        }
    }
}
