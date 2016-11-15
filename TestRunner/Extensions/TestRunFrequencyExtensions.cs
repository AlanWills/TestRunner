using System;
using System.Diagnostics;

namespace TestRunner.Extensions
{
    public static class TestRunFrequencyExtensions
    {
        public static string ToDisplayString(this TestRunFrequency frequency)
        {
            switch (frequency)
            {
                case TestRunFrequency.Daily:
                    return "Daily";
                default:
                    Debug.Fail("Unhandled frequency type");
                    return "";
            }
        }

        public static TestRunFrequency ToTestRunFrequency(this string frequency)
        {
            switch (frequency)
            {
                case "Daily":
                    return TestRunFrequency.Daily;
                default:
                    Debug.Fail("Unhandled frequency type");
                    return TestRunFrequency.Daily;
            }
        }

        public static TimeSpan ToTimeSpan(this TestRunFrequency frequency)
        {
            switch (frequency)
            {
                case TestRunFrequency.Daily:
                    return TimeSpan.FromDays(1);

                default:
                    Debug.Assert(false, "Unhandled TestRunFrequency value");
                    return TimeSpan.FromMilliseconds(-1);
            }
        }
    }
}
