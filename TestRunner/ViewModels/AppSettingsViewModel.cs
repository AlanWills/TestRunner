using TestRunner.Models;

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
