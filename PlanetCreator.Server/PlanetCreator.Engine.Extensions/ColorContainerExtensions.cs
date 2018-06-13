using System.Drawing;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;

namespace PlanetCreator.Engine.Extensions
{
    public static class ColorContainerExtensions
    {
        public static void GeneratePreview(this ColorContainer container, Size size, Stream stream)
        {
            var range = container.MaxLevel - container.MinLevel;

            var colors = new Color[range];

            for (int level = container.MinLevel, i = 0; level < container.MaxLevel; level++, i++)
                colors[i] = container.GetColor(level);

            using (var image = new Image<Rgba32>(size.Width, range))
            {
                for (var x = 0; x < size.Width; x++)
                for (var y = 0; y < range; y++)
                {
                    var c = colors[y];
                    image[x, y] = new Rgba32(c.R, c.G, c.B, c.A);
                }

                image.Mutate(e => e.Resize(size.Width, size.Height));
                image.SaveAsPng(stream);
            }
        }
    }
}