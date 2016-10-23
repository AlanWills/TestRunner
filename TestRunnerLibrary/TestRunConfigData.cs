using System.Xml;
using System.Xml.Serialization;

namespace TestRunnerLibrary
{
    public class TestRunConfigData
    {
        public string FullPathToDll { get; set; }

        public string OutputFileFullPath { get; set; }

        public string ErrorFileFullPath { get; set; }

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
