using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using TestRunner.Models;

namespace TestRunner
{
    public class TestResult
    {
        #region Properties and Fields

        public const string FileExtension = ".ftr";

        public DateTime DateOfTesting { get; private set; }

        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(FilePath); }
        }

        /// <summary>
        /// The full filepath of the test results file
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// True if all of the tests passed.  Otherwise false.
        /// </summary>
        public bool Passed { get; private set; }

        public List<UnitTestResult> UnitTests { get; private set; }

        #endregion

        public TestResult(string testResultFilePath)
        {
            Debug.Assert(File.Exists(testResultFilePath), "Results file path: " + testResultFilePath + " does not exist");
            FilePath = testResultFilePath;

            UnitTests = new List<UnitTestResult>();

            ReadTestFile();
        }

        private void ReadTestFile()
        {
            XmlDocument document = new XmlDocument();
            document.Load(FilePath);

            ReadOutcome(document);
            ReadDate(document);
            ReadUnitTests(document);
        }

        private void ReadOutcome(XmlDocument document)
        {
            XmlNodeList nodeList = document.GetElementsByTagName("ResultSummary");
            Debug.Assert(nodeList.Count == 1, "Multiple test result nodes found");

            XmlNode resultsNode = nodeList.Item(0);
            Debug.Assert(resultsNode.HasChildNodes);

            XmlNode countersNode = resultsNode.FirstChild;

            XmlNode totalAttribute = countersNode.Attributes.GetNamedItem("total");
            string totalTestsString = totalAttribute.Value;

            XmlNode passedAttribute = countersNode.Attributes.GetNamedItem("passed");
            string passedTestsString = passedAttribute.Value;

            Passed = totalTestsString == passedTestsString;
        }

        private void ReadDate(XmlDocument document)
        {
            XmlNodeList nodeList = document.GetElementsByTagName("Times");
            Debug.Assert(nodeList.Count == 1, "Multiple times nodes found");

            XmlNode timesNode = nodeList.Item(0);
            XmlNode createdAttribute = timesNode.Attributes.GetNamedItem("creation");

            DateTime datetime;
            bool result = DateTime.TryParse(createdAttribute.Value, out datetime);
            Debug.Assert(result, "Failed to parse time of creation");

            DateOfTesting = datetime;
        }

        private void ReadUnitTests(XmlDocument document)
        {
            XmlNodeList nodeList = document.GetElementsByTagName("Results");
            Debug.Assert(nodeList.Count == 1, "Multiple test result nodes found");

            XmlNode resultsNode = nodeList.Item(0);
            Debug.Assert(resultsNode.HasChildNodes);

            foreach (XmlNode unitTestResult in resultsNode)
            {
                string testName = unitTestResult.Attributes.GetNamedItem("testName").Value;
                bool passed = unitTestResult.Attributes.GetNamedItem("outcome").Value == "Passed";

                UnitTestResult result = new UnitTestResult(testName, passed);
                UnitTests.Add(result);
            }
        }
    }
}
