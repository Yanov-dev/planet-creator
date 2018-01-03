using System.Drawing;

namespace planet_craator.engine.Extionsions
{
    public static class ColorExtiontions
    {
        public static Color Lerp(this Color c1, Color c2, double lerp)
        {
            if (lerp < 0 || lerp > 1)
            {
                lerp = 0.5;
            }

            return Color.FromArgb(
                (int) (c2.A * lerp + c1.A * (1 - lerp)),
                (int) (c2.R * lerp + c1.R * (1 - lerp)),
                (int) (c2.G * lerp + c1.G * (1 - lerp)),
                (int) (c2.B * lerp + c1.B * (1 - lerp)));
        }
    }
}