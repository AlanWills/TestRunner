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

namespace TestRunner
{
    public static class TestRunConfigDataExtensions
    {

        /// <summary>
        /// Use this when you do not have permissions to read the file using ordinary IO
        /// </summary>
        /// <param name="configDataFile"></param>
        /// <returns></returns>
        public static async Task<TestRunConfigData> DeserializeAsync(StorageFile configDataFile)
        {
            // Oh sweet mother of god.  Just follow the compiler, shut your eyes and hope to god this works
            return await Task.Run(async () =>
            {
                using (Stream stream = await configDataFile.OpenStreamForReadAsync())
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                        return (TestRunConfigData)serializer.Deserialize(reader);
                    }
                }
            });
        }

        public static async void SerializeAsync(this TestRunConfigData data, StorageFile fileToSaveInto)
        {
            using (Stream stream = await fileToSaveInto.OpenStreamForWriteAsync())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                serializer.Serialize(stream, data);
            }
        }
    }
}
