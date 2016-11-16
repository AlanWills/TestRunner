using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
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

    public class Project : IXmlSerializable
    {
        #region Properties and Fields

        public const string FileExtension = ".ftp";

        public string FilePath { get; set; }

        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public TestRunFrequency Frequency { get; set; }

        public string FullPathToDll { get; set; }

        public Platform Platform { get; set; }

        public List<TestResult> TestResults { get; private set; }
        
        #endregion

        public Project()
        {
            TestResults = new List<TestResult>();
        }

        /// <summary>
        /// Creates a test run process using this project
        /// </summary>
        public void Run()
        {
            TestRunnerProcessManager.CreateProcess(this);
            StartTime = DateTime.Now;
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

        #region ISerializable Interface

        public XmlSchema GetSchema()
        {
            // Apparently this is legit
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            Name = reader.ReadElementContentAsString();
            StartTime = DateTime.Parse(reader.ReadElementContentAsString());
            Frequency = (TestRunFrequency)Enum.Parse(typeof(TestRunFrequency), reader.ReadElementContentAsString());
            FullPathToDll = reader.ReadElementContentAsString();
            Platform = (Platform)Enum.Parse(typeof(Platform), reader.ReadElementContentAsString());

            reader.Read();

            while (reader.Name == "TestResult")
            {
                TestResults.Add(new TestResult(reader.ReadElementContentAsString()));
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Name", Name);
            writer.WriteElementString("StartTime", StartTime.ToLongDateString());
            writer.WriteElementString("Frequency", Frequency.ToString());
            writer.WriteElementString("FullPathToDll", FullPathToDll);
            writer.WriteElementString("Platform", Platform.ToString());
            writer.WriteStartElement("TestResults");

            foreach (TestResult result in TestResults)
            {
                writer.WriteElementString("TestResult", result.FilePath);
            }

            writer.WriteEndElement();
        }

        #endregion
    }
}