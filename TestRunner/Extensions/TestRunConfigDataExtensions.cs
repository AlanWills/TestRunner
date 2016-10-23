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
        public static async void SerializeAsync(this TestRunConfigData data, StorageFile fileToSaveInto)
        {
            using (StorageStreamTransaction stream = await fileToSaveInto.OpenTransactedWriteAsync())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                serializer.Serialize(stream.Stream.AsStream(), data);
            }
        }
    }
}
