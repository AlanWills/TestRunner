using System.IO;

namespace TestRunnerUnitTests
{
    public static class Resources
    {
        public static string ResourcesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Resources");
        public static string DummyCSharpDll = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "DummyTestProjectsForTesting", "DummyCSharpTestProject", "bin", "Debug", "DummyCSharpTestProject.dll");
        public static string ProjectSaveLocation = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "TestFiles");
    }
}
