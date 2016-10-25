using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TestRunnerLibrary;
using Windows.Storage;

namespace TestRunnerLibrary
{
    public enum TestRunFrequency
    {
        kDaily
    }

    public class TestRunConfigData
    {
        public string ProcessName { get; set; }

        public DateTime StartTime { get; set; }

        public TestRunFrequency Frequency { get; set; }

        public string FullPathToDll { get; set; }

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
