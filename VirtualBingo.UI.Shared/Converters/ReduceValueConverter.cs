using System;
using System.Globalization;
using System.Windows.Data;

namespace VirtualBingo.UI.Shared.Converters
{
    public class ReduceValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double)
            {
                double d = (double) value;

                return d * 0.5;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
