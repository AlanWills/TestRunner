using System;
using System.Globalization;
using System.Windows.Data;

namespace TestRunner.Converters
{

    /// <summary>
    /// A class which assumes an enum is of the form kValue1, kValue2 etc.
    /// Will convert to a string by removing the k and back by adding the k.
    /// Therefore, kValue1 -> "Value1" & "Value1" -> kValue1
    /// </summary>
    public class DefaultEnumConverter : IValueConverter
    {
        /// <summary>
        /// Convert enum to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert to a string, but miss off the first letter which we assume to be 'k'
            return value.ToString().Substring(1);
        }

        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(targetType, "k" + (value as string));
        }
    }
}
