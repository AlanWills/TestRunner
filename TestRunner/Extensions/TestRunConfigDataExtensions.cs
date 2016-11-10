using System.IO;
using System.Xml.Serialization;
using TestRunnerLibrary;

namespace TestRunner
{
    public static class TestRunConfigDataExtensions
    {
        /// <summary>
        /// Serializes the data out into the file path.
        /// The file will be created if not already there and overwritten if it does already exist.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileToSaveInto"></param>
        public static void Serialize(this TestRunConfigData data, string fileToSaveInto)
        {
            using (FileStream stream = new FileStream(fileToSaveInto, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                serializer.Serialize(stream, data);
            }
        }
    }
}
