using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunnerLibrary
{
    public static class Extensions
    {
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
