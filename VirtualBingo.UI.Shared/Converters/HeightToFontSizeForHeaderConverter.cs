using System;
using System.Globalization;
using System.Windows.Data;

namespace VirtualBingo.UI.Shared.Converters
{
    public class HeightToFontSizeForHeaderConverter : IValueConverter
    {
        private const double StartFontValue = 16;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double))
            {
                return StartFontValue;
            }

            double height = (double) value, 
                   increment = height / 600 - (height / 600 * 0.2);

            if (increment <= 1)
            {
                increment = 1;
            }
            else if(increment > 1.5)
            {
                increment = 1.5;
            }

            return StartFontValue * increment;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
