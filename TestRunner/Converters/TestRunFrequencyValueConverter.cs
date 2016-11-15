using System;
using System.Globalization;
using System.Windows.Data;
using TestRunner.Extensions;

namespace TestRunner.Converters
{
    public class TestRunFrequencyValueConverter : IValueConverter
    {
        // Convert TestRunFrequency to string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TestRunFrequency)value).ToDisplayString();
        }

        // Convert string to TestRunFrequency
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string).ToTestRunFrequency();
        }
    }
}
