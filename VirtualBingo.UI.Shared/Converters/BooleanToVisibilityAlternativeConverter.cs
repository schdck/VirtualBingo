using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace VirtualBingo.UI.Shared.Converters
{
    public class BooleanToVisibilityAlternativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as bool? == true ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
