using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using TestRunnerLibrary;

namespace TestRunnerServiceLibrary
{
    // Extension of the process class which reroutes output and povides extra custom functionality for running tests
    // Also wraps up all of the process logic for starting, reading output/error and writing to the output files
    // It should only be created from the TestRunnerProcessManager class
    internal class TestRunnerProcess : Process
    {
        #region Properties and Fields

        /// <summary>
        /// The timer which will ensure the process repeats itself based on the value we gave it
        /// </summary>
        private Timer Timer { get; set; }

        public string ConfigFilePath { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// Diverted output string for the Process Output so that we can write it to a file at the end without worrying about concurrency issues with the file
        /// </summary>
        public StringBuilder Output { get; set; }

        /// <summary>
        /// Diverted output string for the Process Error so that we can write it to a file at the end without worrying about concurrency issues with the file
        /// </summary>
        public StringBuilder Error { get; set; }

        private TestRunConfigData Data { get; set; }

        #endregion

        internal TestRunnerProcess(string configDataFilePath)
        {
            ConfigFilePath = configDataFilePath;
            Data = TestRunConfigData.Deserialize(configDataFilePath);

            Name = Data.ProcessName;
            EnableRaisingEvents = true;

            StartInfo = CreateCmdLineProcessStartInfo(Path.GetDirectoryName(Data.FullPathToDll), Path.GetFileName(Data.FullPathToDll));

            Output = new StringBuilder();
            Error = new StringBuilder();

            OutputDataReceived += TestRunnerProcess_OutputDataReceived;
            ErrorDataReceived += TestRunnerProcess_ErrorDataReceived;
            Exited += WriteErrorAndOutputToFiles;

            Timer = new Timer(RerunTestProcess, 0, TimeSpan.FromMilliseconds(0), Data.Frequency.ToTimeSpan());
        }

        private void RerunTestProcess(object state)
        {
            Output.Clear();
            Error.Clear();

            bool startResult = Start();
            Debug.Assert(startResult);

            BeginOutputReadLine();
            BeginErrorReadLine();
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
            cmdInfo.RedirectStandardError = true;
            cmdInfo.RedirectStandardOutput = true;
            cmdInfo.UseShellExecute = false;
            cmdInfo.Arguments = dllName + " /inIsolation /Platform:" + Data.Platform.ToString() + " /Logger:trx";
            cmdInfo.WorkingDirectory = workingDirectory;
            cmdInfo.FileName = ServiceSettings.VSTestPath;

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
            CancelOutputRead();
            CancelErrorRead();

            using (FileStream fileStream = new FileStream(Data.OutputFileFullPath, FileMode.OpenOrCreate))
            {
                // Blocking write the output
                fileStream.Write(Encoding.ASCII.GetBytes(Output.ToString()), 0, Output.Length);
            }

            using (FileStream fileStream = new FileStream(Data.ErrorFileFullPath, FileMode.OpenOrCreate))
            {
                // Blocking write the error
                fileStream.Write(Encoding.ASCII.GetBytes(Error.ToString()), 0, Error.Length);
            }
        }
    }
}
