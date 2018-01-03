using System.IO;
using planet_craator.engine;
using SixLabors.ImageSharp;

namespace planet_creator.engine.extensions
{
    public static class PlanetExtensions
    {
        public static void ToPng(this Planet2D planet2D, Stream stream)
        {
            var width = planet2D.Width;
            var height = planet2D.Height;
            var colors = planet2D.Colors;
            
            using (var image = new Image<Rgba32>(width, height))
            {
                for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    var c = colors[x, y];
                    image[x, y] = new Rgba32(c.R, c.G, c.B, c.A);
                }

                image.SaveAsPng(stream);
            }
        }
    }
}