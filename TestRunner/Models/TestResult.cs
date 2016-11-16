using System.IO;

namespace TestRunner
{
    public class TestResult
    {
        #region Properties and Fields

        public const string FileExtension = ".ftr";

        public string Name
        {
            get { return Path.GetFileNameWithoutExtension(FilePath); }
        }

        /// <summary>
        /// The full filepath of the test results file
        /// </summary>
        public string FilePath { get; private set; }

        #endregion

        public TestResult(string testResultFilePath)
        {
            FilePath = testResultFilePath;
        }
    }
}
