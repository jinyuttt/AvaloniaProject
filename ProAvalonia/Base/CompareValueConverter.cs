using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace ProAvalonia.Base
{
    internal class CompareValueConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(values);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = double.Parse(values[0].ToString());
            var width = double.Parse(values[1].ToString());
            return value / 240 * width;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
