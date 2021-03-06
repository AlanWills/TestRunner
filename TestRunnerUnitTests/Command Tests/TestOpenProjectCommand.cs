﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TestExtensions;
using TestRunner;
using TestRunner.Commands;

namespace TestRunnerUnitTests
{
    [TestClass]
    public class TestOpenProjectCommand
    {
        private static string ResourcesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Resources");

        [TestInitialize]
        public void TestInitialize()
        {
            ProjectManager.Initialize();
        }

        [TestMethod]
        public void Open_CallsOnLoadedEvent()
        {
            bool value = false;
            OpenProjectCommand.ProjectLoaded += delegate (Project project)
            {
                value = true;
            };

            OpenProjectCommand openCommand = new OpenProjectCommand();
            openCommand.Execute(Path.Combine(ResourcesDirectory, "NoTestResults" + Project.FileExtension));

            Assert.IsTrue(value);
        }

        [TestMethod]
        public void OpenSimple_ProjectHasNoTestResults()
        {
            OpenProjectCommand openCommand = new OpenProjectCommand();
            openCommand.Execute(Path.Combine(ResourcesDirectory, "NoTestResults" + Project.FileExtension));

            Project loaded = ProjectManager.CurrentProject;

            Assert.AreEqual("No Test Results", loaded.Name);
            Assert.AreEqual(Platform.x86, loaded.Platform);
            Assert.AreEqual(TimeSpan.Zero, loaded.Frequency);
            
            string expectedFileName = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "DummyTestProjectsForTesting", "DummyCSharpTestProject", "bin", "Debug", "DummyCSharpTestProject.dll");
            Assert.IsTrue(Path.GetFileName(expectedFileName) == Path.GetFileName(loaded.FullPathToDll));
            Assert.AreEqual(0, loaded.TestResults.Count);
        }

        [TestMethod]
        public void OpenComplex_ProjectHasTestResults()
        {
            OpenProjectCommand openCommand = new OpenProjectCommand();
            openCommand.Execute(Path.Combine(ResourcesDirectory, "TestResults" + Project.FileExtension));

            Project loaded = ProjectManager.CurrentProject;

            Assert.AreEqual("Test Results", loaded.Name);
            Assert.AreEqual(Platform.x86, loaded.Platform);
            Assert.AreEqual(TimeSpan.Zero, loaded.Frequency);

            string expectedFileName = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "DummyTestProjectsForTesting", "DummyCSharpTestProject", "bin", "Debug", "DummyCSharpTestProject.dll");
            Assert.IsTrue(Path.GetFileName(expectedFileName) == Path.GetFileName(loaded.FullPathToDll));
            Assert.AreEqual(3, loaded.TestResults.Count);

            List<TestResult> expected = new List<TestResult>()
            {
                new TestResult(@"C:\Users\alawi\Source\Repos\TestRunner\TestRunnerUnitTests\Resources\TestResult1.ftr"),
                new TestResult(@"C:\Users\alawi\Source\Repos\TestRunner\TestRunnerUnitTests\Resources\TestResult2.ftr"),
                new TestResult(@"C:\Users\alawi\Source\Repos\TestRunner\TestRunnerUnitTests\Resources\TestResult3.ftr"),
            };

            AssertExt.ContentsEqual<List<TestResult>, TestResult>(expected, loaded.TestResults, TestRunnerEqualityFunctions.TestResultEquality);
        }
    }
}
