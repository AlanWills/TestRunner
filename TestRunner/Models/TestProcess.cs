using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace TestRunner
{
    // Extension of the process class which reroutes output and povides extra custom functionality for running tests
    // Also wraps up all of the process logic for starting, reading output/error and writing to the output files
    // It should only be created from the TestRunnerProcessManager class
    public class TestProcess : Process
    {
        #region Properties and Fields

        /// <summary>
        /// The timer which will ensure the process repeats itself based on the value we gave it
        /// </summary>
        private Timer Timer { get; set; }

        public string Name { get; private set; }
        
        private Project Project { get; set; }

        #endregion

        public TestProcess(Project projectToTest)
        {
            Project = projectToTest;

            Name = Project.Name;
            EnableRaisingEvents = true;

            StartInfo = CreateCmdLineProcessStartInfo(Path.GetDirectoryName(Project.FullPathToDll), Path.GetFileName(Project.FullPathToDll));

            Timer = new Timer(RerunTestProcess, 0, TimeSpan.FromMilliseconds(0), Project.Frequency);
            Exited += MoveResultsFile;

            // If we are specifying continuous building, we need to add an extra event to fire up the process again
            if (Project.Frequency == TimeSpan.FromMilliseconds(-1))
            {
                Exited += ContinuousBuilding;
            }
        }

        private void MoveResultsFile(object sender, EventArgs e)
        {
            string testResultsDirectoryPath = Path.Combine(Directory.GetParent(Project.FullPathToDll).FullName, "TestResults");
            DirectoryInfo info = new DirectoryInfo(testResultsDirectoryPath);
            FileInfo testResults = info.GetFiles("*.trx", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime).First();

            // Copy to the project folder
            testResultsDirectoryPath = Path.Combine(Directory.GetParent(Project.FilePath).FullName, Project.Name + " TestResults");
            DirectoryInfo projectResults = Directory.CreateDirectory(testResultsDirectoryPath);

            string testResultNewFileName = Project.Name + " " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + TestResult.FileExtension;
            testResultNewFileName = testResultNewFileName.Replace(":", "_");

            string testResultsNewPath = Path.Combine(projectResults.FullName, testResultNewFileName);
            testResults.MoveTo(testResultsNewPath);

            Project.AddTestResult(new TestResult(testResultsNewPath));
        }

        private void RerunTestProcess(object state)
        {
            bool startResult = Start();
            Debug.Assert(startResult);
        }

        private void ContinuousBuilding(object sender, EventArgs e)
        {
            // Just sleep for a minute between continuous builds to let everything wash through
            Thread.Sleep(60000);

            RerunTestProcess(null);
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
