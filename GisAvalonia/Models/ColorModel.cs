

using Avalonia.Media;
using ReactiveUI;

namespace GisAvalonia.Models
{
    public class ColorModel: ReactiveObject
    {
        private IBrush _color = Brushes.ForestGreen;

        public IBrush Color
        {
           get { return _color; }
            set { this.RaiseAndSetIfChanged(ref _color, value); }
        }

    }
}
