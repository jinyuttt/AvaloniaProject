using System.Drawing;

namespace Mapsui.UI.Render.Extensions;

public static class ColorExtensions
{
    /// <summary>
    /// Convert Mapsui.Styles.Color to Microsoft.Maui.Graphics.Color
    /// </summary>
    /// <param name="color">Color in Mapsui format</param>
    /// <returns>Color in Microsoft.Maui.Graphics format</returns>
    public static Color ToMaui(this Styles.Color color)
    {
        return color.ToNative();
    }

    public static Color ToNative(this Styles.Color color)
    {
        return Color.FromArgb(color.A, color.R, color.R, color.B);
    }

    /// <summary>
    /// Convert Microsoft.Maui.Graphics.Color to Mapsui.Style.Color
    /// </summary>
    /// <param name="color">Color in Microsoft.Maui.Graphics.Color format </param>
    /// <returns>Color in Mapsui.Styles.Color format</returns>
    public static Styles.Color ToMapsui(this Color color)
    {
        return new Styles.Color(color.R,color.G,color.B,color.A);
    }
}
