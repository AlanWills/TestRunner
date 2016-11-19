namespace TestRunner.Models
{
    public struct UnitTestResult
    {
        public string Name { get; private set; }
        public bool Passed { get; private set; }

        public UnitTestResult(string name, bool passed)
        {
            Name = name;
            Passed = passed;
        }
    }
}
