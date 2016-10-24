namespace TestRunnerLibrary
{
    public static class ServiceSettings
    {
        #region Properties and Fields

        public static string VSTestPath { get; set; }

        #endregion

        static ServiceSettings()
        {
            VSTestPath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Vstest.console.exe";
        }
    }
}
