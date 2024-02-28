using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Rendering;
using Mapsui.Rendering.Skia.SkiaStyles;
using Mapsui.Styles;
using SkiaSharp;
using System;

namespace MapAvalonia.Models
{
    public class SkiaCustomStyleRenderer : ISkiaStyleRenderer
    {
        public static Random rnd = new Random();
      

        public bool Draw(SKCanvas canvas, Viewport viewport, ILayer layer, IFeature feature, IStyle style, IRenderCache renderCache, long iteration)
        {
            //if (feature.RenderedGeometry.Count==0)
            //    return false;

            if (style is CustomStyle styleCustom)
            {
                Console.WriteLine("123");
            }
         
            MRect worldPoint=new MRect(feature.Extent.MinX,feature.Extent.MinY, feature.Extent.MaxX+100, feature.Extent.MaxY+100);
            worldPoint= new MRect(10340390, 7554750, 10340735, 7554670);
            var screenPoint = viewport.WorldToScreen(worldPoint);
            var color = new SKColor((byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)rnd.Next(0, 256), (byte)(256.0 * layer.Opacity * style.Opacity));
            var colored = new SKPaint() { Color = color, IsAntialias = true };
            var black = new SKPaint() { Color = SKColors.Black, IsAntialias = true };

            canvas.Translate((float)screenPoint.Width, (float)screenPoint.Height);
            canvas.DrawCircle(0, 0, 15, colored);
            canvas.DrawCircle(-8, -12, 8, colored);
            canvas.DrawCircle(8, -12, 8, colored);
            canvas.DrawCircle(8, -8, 2, black);
            canvas.DrawCircle(-8, -8, 2, black);
            using (var path = new SKPath())
            {
                path.ArcTo(new SKRect(-8, 2, 8, 10), 25, 135, true);
                canvas.DrawPath(path, new SKPaint() { Style = SKPaintStyle.Stroke, Color = SKColors.Black, IsAntialias = true });
            }

            return true;
        }
    }
}
