﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using TestRunner.Extensions;

namespace TestRunner
{
    public delegate void ProjectChangedEvent(ProjectChangedEventArgs projectChangedArgs);

    public class ProjectChangedEventArgs : EventArgs
    {
        public Project ChangedProject { get; private set; }
        public List<TestResult> AddedTests { get; private set; }
        public List<TestResult> RemovedTests { get; private set; }

        public ProjectChangedEventArgs(Project project, List<TestResult> addedTests, List<TestResult> removedTests)
        {
            ChangedProject = project;
            AddedTests = addedTests;
            RemovedTests = removedTests;
        }
    }

    public enum TestRunFrequency
    {
        Daily,
        Hourly,
        Continuously
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

        public event ProjectChangedEvent ProjectChanged;
        
        #endregion

        public Project()
        {
            TestResults = new List<TestResult>();
            Frequency = TestRunFrequency.Daily.ToTimeSpan();
        }

        /// <summary>
        /// Creates a test run process using this project
        /// </summary>
        public void Run()
        {
            TestProcessManager.StartProcess(this);
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Adds the inputted test result to this project and saves the file.
        /// Will also fire the ProjectChanged event.
        /// </summary>
        /// <param name="testResult"></param>
        public void AddTestResult(TestResult testResult)
        {
            TestResults.Add(testResult);
            Save();

            ProjectChangedEventArgs args = new ProjectChangedEventArgs(this, new List<TestResult>() { testResult }, new List<TestResult>());

            // Make sure to invoke the ProjectChanged event on the UI thread as it will be used to update UI.
            Application.Current.Dispatcher.Invoke(() => ProjectChanged?.Invoke(args));
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

            reader.SkipWhiteSpace();
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

            indent += "\t";
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