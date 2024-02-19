using Avalonia.Controls;
using System.ComponentModel;
using System;
using Avalonia;
using System.Drawing;
using Avalonia.Media;
using Brush = Avalonia.Media.Brush;


namespace ProAvalonia.Controls
{
    public partial class CircularProgressBar : UserControl
    {
        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<CircularProgressBar,double>(nameof(Value),0);
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        
        }
       

        public static readonly StyledProperty<Brush> BackColorProperty =
           AvaloniaProperty.Register<CircularProgressBar, Brush>(nameof(BackColor));
        public Brush BackColor
        {
            get { return (Brush)GetValue(BackColorProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly StyledProperty<string> TitleProperty =
          AvaloniaProperty.Register<CircularProgressBar, string>(nameof(Title));

        public CircularProgressBar()
        {
            InitializeComponent();
            this.SizeChanged += CircularProgressBar_SizeChanged;
        }

      
        private void CircularProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            this.layout.Width = Math.Min(this.Width, this.Height);
            double radius = this.layout.Width / 2;
            if (radius == 0 || Value == 0) return;

            double newX = 0.0, newY = 0.0;
            newX = radius + (radius - 3) * Math.Cos((Value % 100 * 100 * 3.6 - 90) * Math.PI / 180);
            newY = radius + (radius - 3) * Math.Sin((Value % 100 * 100 * 3.6 - 90) * Math.PI / 180);

            string pathStr = $"M{radius + 0.01} 3 " +
                $"A{radius - 3} {radius - 3} 0 {(this.Value < 0.5 ? 0 : 1)} 1 {newX} {newY}";

            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            this.path.Data = (Geometry)converter.ConvertFrom(pathStr);
        }
    }
}
