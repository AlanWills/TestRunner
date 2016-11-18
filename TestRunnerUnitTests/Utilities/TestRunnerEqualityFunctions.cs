using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExtensions;
using TestRunner;

namespace TestRunnerUnitTests
{
    public static class TestRunnerEqualityFunctions
    {
        public static bool TestResultEquality(TestResult expected, TestResult actual)
        {
            return EqualityFunctions.PathEquality(expected.FilePath, actual.FilePath);
        }
    }
}
