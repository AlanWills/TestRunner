﻿using System;
using System.Xml;
using System.Xml.Serialization;

namespace TestRunnerLibrary
{
    public enum TestRunFrequency
    {
        kDaily,
    }

    public enum Platform
    {
        kx86,
        kx64,
        kARM,
        kAnyCPU
    }

    public class TestRunConfigData
    {
        public string ProcessName { get; set; }

        public DateTime StartTime { get; set; }

        public TestRunFrequency Frequency { get; set; }

        public string FullPathToDll { get; set; }

        public Platform Platform { get; set; }

        public string OutputFileFullPath { get; set; }

        public string ErrorFileFullPath { get; set; }

        /// <summary>
        /// Use this if you do have permissions to read the file with ordinary IO
        /// </summary>
        /// <param name="configDataFile"></param>
        /// <returns></returns>
        public static TestRunConfigData Deserialize(string configDataFilePath)
        {
            using (XmlReader reader = XmlReader.Create(configDataFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                return (TestRunConfigData)serializer.Deserialize(reader);
            }
        }
    }
}
