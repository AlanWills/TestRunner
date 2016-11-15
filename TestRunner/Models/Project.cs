using System;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace TestRunner
{
    public enum TestRunFrequency
    {
        Daily,
    }

    public enum Platform
    {
        x86,
        x64,
    }

    public class Project
    {
        #region Properties and Fields

        public const string FileExtension = ".frp";

        public string FilePath { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public TestRunFrequency Frequency { get; set; }

        public string FullPathToDll { get; set; }

        public Platform Platform { get; set; }
        
        #endregion

        /// <summary>
        /// Creates a test run process using this project
        /// </summary>
        public void Run()
        {
            TestRunnerProcessManager.CreateProcess(this);
        }

        /// <summary>
        /// Saves the project to disc.
        /// Overwrites the project if it already exists.
        /// </summary>
        /// <param name="projectFilePath"></param>
        public void Save()
        {
            using (XmlWriter writer = XmlWriter.Create(FilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                serializer.Serialize(writer, this);
            }
        }
    }
}