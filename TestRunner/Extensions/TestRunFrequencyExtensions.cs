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
                case TestRunFrequency.Hourly:
                    return "Hourly";
                case TestRunFrequency.Continuously:
                    return "Continuously";
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
                case "Hourly":
                    return TestRunFrequency.Hourly;
                case "Continuously":
                    return TestRunFrequency.Continuously;
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
                case TestRunFrequency.Hourly:
                    return TimeSpan.FromHours(1);
                case TestRunFrequency.Continuously:
                    return TimeSpan.FromMilliseconds(-1);
                default:
                    Debug.Assert(false, "Unhandled TestRunFrequency value");
                    return TimeSpan.FromMilliseconds(-1);
            }
        }

        public static TestRunFrequency ToTestRunFrequency(this TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromDays(1))
            {
                return TestRunFrequency.Daily;
            }
            else if (timeSpan == TimeSpan.FromHours(1))
            {
                return TestRunFrequency.Hourly;
            }
            else if (timeSpan == TimeSpan.FromMilliseconds(-1))
            {
                return TestRunFrequency.Continuously;
            }

            // Should probably put custom here
            return TestRunFrequency.Daily;
        }
    }
}
