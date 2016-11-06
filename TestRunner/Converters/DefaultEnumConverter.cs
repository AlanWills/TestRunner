using System;
using System.Globalization;
using System.Windows.Data;

namespace TestRunner.Converters
{

    /// <summary>
    /// A class which assumes an enum is of the form Value1, Value2 etc.
    /// Will convert to a string by calling ToString().
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
            return value.ToString();
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
            return Enum.Parse(targetType, (value as string));
        }
    }
}
