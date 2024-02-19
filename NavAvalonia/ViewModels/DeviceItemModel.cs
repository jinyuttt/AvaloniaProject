using Avalonia.Controls;
using Avalonia.Media.Imaging;
using NavAvalonia.Converters;
using ReactiveUI;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace NavAvalonia.ViewModels
{
    public class DeviceItemModel:ViewModelBase
    {
        public List<DevicePropertyItemModel> Properties { get; set; } = new List<DevicePropertyItemModel>();
     
        public object LightType { get; internal set; }
        public string Title { get; internal set; }

        private string url = "";
        public string ImageUrl {
            get { return url; }
            set
            {
                this.RaiseAndSetIfChanged(ref url, value);
               this.Image=(Bitmap)BitmapAssetValueConverter.Instance.Convert(value, typeof(Bitmap),null,default(CultureInfo));
            }
 
}

        public Bitmap Image { get; internal set; } 
    }
}