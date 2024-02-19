using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Drawing;
using System.Globalization;
using Brush = Avalonia.Media.Brush;


namespace ProAvalonia.Base
{
    internal class BrushToColorConvert : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null) return null;
            ColorConverter colorConverter = new ColorConverter();
            System.Drawing.Color v = (System.Drawing.Color)colorConverter.ConvertFromString((string)parameter);
            BrushConverter brushConverter = new BrushConverter();
        
            Avalonia.Media.Color color = new Avalonia.Media.Color(v.A,v.R,v.G,0);
             Brush brush = new SolidColorBrush(color);
            // Brush brush = (Brush)brushConverter.ConvertFromString(color));
            return brush;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
