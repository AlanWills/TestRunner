using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TestExtensions;
using TestRunner;

namespace TestRunnerUnitTests
{
    [TestClass]
    public class TestNewProjectDialogViewModel
    {
        #region Properties and Fields

        private NewProjectDialogViewModel NewProjectDialog { get; set; }

        public TestContext TestContext;

        #endregion

        [ClassInitialize]
        public static void TestInitialize(TestContext testContext)
        {
            Assert.IsTrue(File.Exists(Resources.DummyCSharpDll));
            Assert.IsTrue(Directory.Exists(Resources.ProjectSaveLocation));
        }

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

            NewProjectDialog.ProjectSaveLocation = "Test";
            NewProjectDialog.FullPathToDll = "DllPath";
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);

            NewProjectDialog.ProjectSaveLocation = "Test";
            NewProjectDialog.FullPathToDll = Resources.DummyCSharpDll;
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);

            NewProjectDialog.ProjectSaveLocation = Resources.ProjectSaveLocation;
            NewProjectDialog.FullPathToDll = "DllPath";
            Assert.IsFalse(NewProjectDialog.IsConfigurationValid);
        }

        [TestMethod]
        public void IsConfigurationValid_ShouldBeTrue()
        {
            NewProjectDialog.FullPathToDll = Resources.DummyCSharpDll;
            NewProjectDialog.ProjectSaveLocation = Resources.ProjectSaveLocation;
            Assert.IsTrue(NewProjectDialog.IsConfigurationValid);
        }

        [TestMethod]
        public void CreateProject_WithValidConfiguration()
        {
            NewProjectDialog.FullPathToDll = Resources.DummyCSharpDll;
            NewProjectDialog.ProjectSaveLocation = Resources.ProjectSaveLocation;
            NewProjectDialog.ProjectName = "Test Valid Configuration";

            Assert.IsTrue(NewProjectDialog.IsConfigurationValid);
            NewProjectDialog.CreateProject();

            string expectedFilePath = Path.Combine(Resources.ProjectSaveLocation, "TestValidConfiguration" + Project.FileExtension);
            Assert.IsTrue(File.Exists(expectedFilePath));
        }

        [TestMethod]
        public void CreateProject_WithInvalidConfiguration_RaisesDebugAssertion()
        {
            Delegate createProject = (Action)NewProjectDialog.CreateProject;

            AssertExt.DebugAssertionRaised(createProject);
        }
    }
}
