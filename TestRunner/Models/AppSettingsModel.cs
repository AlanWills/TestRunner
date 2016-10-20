using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Models
{
    public static class AppSettingsModel
    {
        #region Properties and Fields

        public static string MSTestPath { get; set; }

        #endregion

        static AppSettingsModel()
        {
            MSTestPath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\MSTest.exe";
            if (!File.Exists(MSTestPath))
            {
                MSTestPath = "";
            }
        }
    }
}
