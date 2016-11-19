using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TestRunner.Extensions;

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

        public TimeSpan Frequency { get; set; }

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

            reader.SkipWhiteSpace();
            Name = reader.ReadElementContentAsString();

            reader.SkipWhiteSpace();
            StartTime = DateTime.Parse(reader.ReadElementContentAsString());

            reader.SkipWhiteSpace();
            Frequency = TimeSpan.Parse(reader.ReadElementContentAsString());

            reader.SkipWhiteSpace();
            FullPathToDll = reader.ReadElementContentAsString();

            reader.SkipWhiteSpace();
            Platform = (Platform)Enum.Parse(typeof(Platform), reader.ReadElementContentAsString());

            reader.Read();
            reader.Read();  // This is the TestResults node
            reader.SkipWhiteSpace();

            while (reader.Name == "TestResult")
            {
                reader.SkipWhiteSpace();
                TestResults.Add(new TestResult(reader.ReadElementContentAsString()));
                reader.SkipWhiteSpace();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            string indent = "\t";

            writer.WriteWhitespace("\n" + indent);
            writer.WriteElementString("Name", Name);

            writer.WriteWhitespace("\n" + indent);
            writer.WriteElementString("StartTime", StartTime.ToLongDateString());

            writer.WriteWhitespace("\n" + indent);
            writer.WriteElementString("Frequency", Frequency.ToString());

            writer.WriteWhitespace("\n" + indent);
            writer.WriteElementString("FullPathToDll", FullPathToDll);

            writer.WriteWhitespace("\n" + indent);
            writer.WriteElementString("Platform", Platform.ToString());

            writer.WriteWhitespace("\n" + indent);
            writer.WriteStartElement("TestResults");

            indent += "\r";
            foreach (TestResult result in TestResults)
            {
                writer.WriteWhitespace("\n" + indent);
                writer.WriteElementString("TestResult", result.FilePath);
            }
            indent = indent.Remove(indent.Length - 1);

            writer.WriteWhitespace("\n" + indent);
            writer.WriteEndElement();
        }

        #endregion
    }
}