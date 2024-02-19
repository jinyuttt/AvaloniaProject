using System;
using System.Globalization;
using Avalonia;
using System.Reflection;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace NavAvalonia.Converters
{
    public class BitmapAssetValueConverter : IValueConverter
    {
        public readonly static BitmapAssetValueConverter Instance=new BitmapAssetValueConverter();
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string rawUri && targetType.IsAssignableFrom(typeof(Bitmap)))
            {
                Uri uri;

                // Allow for assembly overrides
                if (rawUri.StartsWith("avares://"))
                {
                    uri = new Uri(rawUri);
                }
                else
                {
                    string assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    uri = new Uri($"avares://{assemblyName}/{rawUri}");
                }
                
                var assets = AssetLoader.Open(uri);
              

                return new Bitmap(assets);
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return new Avalonia.Data.BindingNotification(value);
        }
    }
}
