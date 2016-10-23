using TestRunnerLibrary;

namespace TestRunner
{
    public class AppSettingsViewModel
    {
        #region Properties and Fields

        private string vsTestPath = ServiceSettings.VSTestPath;
        public string VSTestPath
        {
            get { return vsTestPath; }
            set
            {
                vsTestPath = value;
                ServiceSettings.VSTestPath = vsTestPath;
            }
        }

        #endregion
    }
}
