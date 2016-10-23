﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunnerService
{
    /// <summary>
    /// Static class which looks after all of the currently running testing process
    /// </summary>
    public static class TestRunnerProcessManager
    {
        #region Properties and Fields

        private static ulong IDCounter = 0;

        private static Dictionary<ulong, TestRunnerProcess> Processes = new Dictionary<ulong, TestRunnerProcess>();

        #endregion

        public static ulong CreateProcess(string configDataFilePath)
        {
            // We should never be creating a process with an ID that already exists in our running processes
            Debug.Assert(!Processes.ContainsKey(IDCounter));

            // Add our process to our dictionary and then return the ID so we can obtain it again later
            Processes.Add(IDCounter, new TestRunnerProcess(configDataFilePath));
            return IDCounter++;
        }

        public static TestingStatus GetProcessStatus(ulong processID)
        {
            return GetProcess(processID).Status;
        }

        public static string GetProcessOutputFilePath(ulong processID)
        {
            return GetProcess(processID).OutputFilePath;
        }

        public static string GetProcessErrorFilePath(ulong processID)
        {
            return GetProcess(processID).ErrorFilePath;
        }

        private static TestRunnerProcess GetProcess(ulong processID)
        {
            // Make sure the requested process exists in our dictionary, otherwise we are in trouble!
            Debug.Assert(Processes.ContainsKey(processID));

            return Processes[processID];
        }
    }
}