using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestRunner;

namespace TestRunnerUnitTests
{
    [TestClass]
    public class TestResultUnitTests
    {
        [TestMethod]
        public void Constructor_ValidFilePath()
        {
            string filePath = Path.Combine(Resources.ResourcesDirectory, "TestResults1" + TestResult.FileExtension);
            TestResult testResult = new TestResult(filePath);

            Assert.IsTrue(testResult.Passed);
            Assert.AreEqual("TestResults1", testResult.Name);
            Assert.AreEqual(filePath, testResult.FilePath);
        }
    }
}
