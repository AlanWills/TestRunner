using System;
using System.Diagnostics;
using System.IO;

namespace TestRunnerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TestRunnerService : ITestRunnerService
    {
        public int StartTesting(string testConfigFilePath)
        {
            PerformCommand();
            return 0;
        }

        public TestingStatus GetTestingStatus(string testConfigFilePath)
        {
            return TestingStatus.kFinished;
        }

        public string GetTestingResultsFilePath(int testingRunID)
        {
            return "";
        }

        private static string MSBuild = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe";
        //"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" OpenGL.sln /p:Platform="Win32"
        //"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Vstest.console.exe" TestKernel.dll

        /// <summary>
        /// Executes a windows command prompt command in a windowless process.
        /// Can provide a callback which will be run after the process is complete.
        /// Asynchronously prints out the standard error and standard output to the Console.
        /// </summary>
        /// <param name="commandAndArgs"></param>
        /// <param name="onCommandCompleteCallback"></param>
        /// <returns></returns>
        private static void PerformCommand()
        {
            ProcessStartInfo cmdInfo = CreateCmdLineProcessStartInfo();
            cmdInfo.FileName = "\"C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\Vstest.console.exe\"";
            RunProcess(cmdInfo);
        }

        /// <summary>
        /// Creates the process start info for running a command in a windowless process in the current working directory with the inputted arguments.
        /// StdError and StdOutput are redirected since it is windowless.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static ProcessStartInfo CreateCmdLineProcessStartInfo(string arguments = "")
        {
            ProcessStartInfo cmdInfo = new ProcessStartInfo();
            cmdInfo.CreateNoWindow = true;
            cmdInfo.RedirectStandardError = true;
            cmdInfo.RedirectStandardOutput = true;
            cmdInfo.UseShellExecute = false;
            cmdInfo.Arguments = "TestKernel.dll";
            cmdInfo.WorkingDirectory = @"C:\Users\Alan\Documents\Visual Studio 2015\Projects\OpenGL\OpenGL\Debug";

            return cmdInfo;
        }

        /// <summary>
        /// Creates a process which outputs error and output asynchronously to the standard output and will block until complete.
        /// </summary>
        /// <param name="processInfo"></param>
        /// <param name="onCommandCompleteCallback"></param>
        private static void RunProcess(ProcessStartInfo processInfo, EventHandler onCommandCompleteCallback = null)
        {
            Process process = new Process();
            process.StartInfo = processInfo;
            process.ErrorDataReceived += PrintToCommandLine;
            process.OutputDataReceived += PrintToCommandLine;

            if (onCommandCompleteCallback != null)
            {
                process.Disposed += onCommandCompleteCallback;
            }

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            process.WaitForExit();
            process.Close();
        }

        private static void PrintToCommandLine(object sender, DataReceivedEventArgs e)
        {
            string str = e.Data;
        }
    }
}
