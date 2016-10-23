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
        public static Task SerializeAsync(this TestRunConfigData data, string configDataFilePath)
        {
            Task task = new Task(() =>
            {
               using (FileStream fileStream = new FileStream(configDataFilePath, FileMode.Create))
               {
                   XmlSerializer serializer = new XmlSerializer(typeof(TestRunConfigData));
                   serializer.Serialize(fileStream, data);
               }
            });

            task.Start();
            return task;
        }
    }
}
