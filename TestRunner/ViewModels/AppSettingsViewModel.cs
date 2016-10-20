using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Models;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace TestRunner
{
    public class AppSettingsViewModel
    {
        #region Properties and Fields

        private string msTestPath = AppSettingsModel.MSTestPath;
        public string MSTestPath
        {
            get { return msTestPath; }
            set
            {
                msTestPath = value;
                AppSettingsModel.MSTestPath = msTestPath;
            }
        }

        #endregion
    }
}
