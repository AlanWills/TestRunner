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
        /// Serializes the data out into the file path
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
