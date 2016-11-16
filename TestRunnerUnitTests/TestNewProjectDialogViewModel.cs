using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestRunner;

namespace TestRunnerUnitTests
{
    [TestClass]
    public class TestNewProjectDialogViewModel
    {
        #region Properties and Fields

        private NewProjectDialogViewModel NewProjectDialog { get; set; }

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            NewProjectDialog = new NewProjectDialogViewModel();
        }

        [TestMethod]
        public void IsConfigurationValid_ShouldBeFalse()
        {
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);

            NewProjectDialog.FullPathToDll = "DllPath";
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);

            NewProjectDialog.ProjectSaveLocation = "Test";
            NewProjectDialog.FullPathToDll = "";
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);
        }

        [TestMethod]
        public void IsConfigurationValid_ShouldBeTrue()
        {
            NewProjectDialog.FullPathToDll = "DllPath";
            NewProjectDialog.ProjectSaveLocation = "Test";
            Assert.IsTrue(NewProjectDialog.IsConfigurationValid);
        }
    }
}
