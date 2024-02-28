using Mapsui.Styles;
using System.Collections.Generic;

namespace MapAvalonia.Models
{
    public class CustomStyle : IStyle
    {
        public double MinVisible { get; set; } = 0;
        public double MaxVisible { get; set; } = double.MaxValue;
        public bool Enabled { get; set; } = true;
        public float Opacity { get; set; } = 0.7f;

        public Dictionary<string, string> Properties { get; set; }
    }
}
