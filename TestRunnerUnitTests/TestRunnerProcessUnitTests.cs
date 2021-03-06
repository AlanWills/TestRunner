﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestRunner;
using System.IO;

namespace TestRunnerUnitTests
{
    [TestClass]
    public class TestRunnerProcessUnitTests
    {
        #region Properties and Fields

        private Project DummyProject { get; set; }

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            DummyProject = new Project();
            DummyProject.FilePath = Directory.GetCurrentDirectory();
            DummyProject.FullPathToDll = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", @"DummyTestProjectsForTesting\DummyCSharpTestProject\bin\Debug\DummyCSharpTestProject.dll");
            Assert.IsTrue(File.Exists(DummyProject.FullPathToDll), "Test DLL does not exist");

            DummyProject.Frequency = TimeSpan.MaxValue;
            DummyProject.Name = "Dummy";
            DummyProject.Platform = Platform.x86;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }

        [TestMethod]
        public void Constructor_ValidProject()
        {
            TestProcess process = new TestProcess(DummyProject);
            process.WaitForExit();
        }
    }
}