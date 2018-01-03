using System.Drawing;
using System.IO;
using planet_craator.engine;
using SixLabors.ImageSharp;

namespace planet_creator.engine.extensions
{
    public static class ColorContainerExtensions
    {
        public static void GeneratePreview(this ColorContainer container, Size size, Stream stream)
        {
            var range = container.MaxLevel - container.MinLevel;

            var colors = new Color[range];

            for (var i = container.MinLevel; i < container.MaxLevel; i++)
                colors[i] = container.GetColor(i);

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