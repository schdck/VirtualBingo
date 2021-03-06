﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace VirtualBingo.UI.Shared.Converters
{
    public class NullOrWhiteSpaceToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && !string.IsNullOrWhiteSpace(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
