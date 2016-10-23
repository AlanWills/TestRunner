using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunnerLibrary;

namespace TestRunnerService
{
    // Extension of the process class which reroutes output and povides extra custom functionality for running tests
    // Also wraps up all of the process logic for starting, reading output/error and writing to the output files
    // It should only be created from the TestRunnerProcessManager class
    internal class TestRunnerProcess : Process
    {
        #region Properties and Fields

        /// <summary>
        /// Diverted output string for the Process Output so that we can write it to a file at the end without worrying about concurrency issues with the file
        /// </summary>
        private StringBuilder Output { get; set; }

        /// <summary>
        /// Diverted output string for the Process Error so that we can write it to a file at the end without worrying about concurrency issues with the file
        /// </summary>
        private StringBuilder Error { get; set; }

        public TestingStatus Status { get; private set; }

        public string OutputFilePath { get; private set; }

        public string ErrorFilePath { get; private set; }

        #endregion

        internal TestRunnerProcess(string configDataFilePath)
        {
            Status = TestingStatus.kRunning;

            TestRunConfigData data = TestRunConfigData.Deserialize(configDataFilePath);

            OutputFilePath = data.OutputFileFullPath;
            ErrorFilePath = data.ErrorFileFullPath;

            EnableRaisingEvents = true;

            StartInfo = CreateCmdLineProcessStartInfo(Path.GetDirectoryName(data.FullPathToDll), Path.GetFileName(data.FullPathToDll));
            StartInfo.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Vstest.console.exe";

            Output = new StringBuilder();
            Error = new StringBuilder();

            OutputDataReceived += TestRunnerProcess_OutputDataReceived;
            ErrorDataReceived += TestRunnerProcess_ErrorDataReceived;
            Exited += WriteErrorAndOutputToFiles;

            bool startResult = Start();
            Debug.Assert(startResult);

            BeginErrorReadLine();
            BeginOutputReadLine();
        }

        /// <summary>
        /// Creates the process start info for running a command in a windowless process in the current working directory with the inputted arguments.
        /// StdError and StdOutput are redirected since it is windowless.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private ProcessStartInfo CreateCmdLineProcessStartInfo(string workingDirectory, string dllName)
        {
            ProcessStartInfo cmdInfo = new ProcessStartInfo();
            cmdInfo.CreateNoWindow = true;
            cmdInfo.RedirectStandardError = true;
            cmdInfo.RedirectStandardOutput = true;
            cmdInfo.UseShellExecute = false;
            cmdInfo.Arguments = dllName;
            cmdInfo.WorkingDirectory = workingDirectory;

            return cmdInfo;
        }

        private void TestRunnerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Output.AppendLine(e.Data);
        }

        private void TestRunnerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Error.AppendLine(e.Data);
        }

        private void WriteErrorAndOutputToFiles(object sender, EventArgs e)
        {
            Status = TestingStatus.kFinished;

            using (FileStream fileStream = new FileStream(OutputFilePath, FileMode.OpenOrCreate))
            {
                // Blocking write the output
                fileStream.Write(Encoding.ASCII.GetBytes(Output.ToString()), 0, Output.Length);
            }

            using (FileStream fileStream = new FileStream(ErrorFilePath, FileMode.OpenOrCreate))
            {
                // Blocking write the error
                fileStream.Write(Encoding.ASCII.GetBytes(Error.ToString()), 0, Error.Length);
            }
        }
    }
}
