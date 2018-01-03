using System.Diagnostics;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using planet_craator.engine;
using SixLabors.ImageSharp;
using Rectangle = System.Drawing.Rectangle;

namespace planet_creator.engine.tests
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void TestByHash()
        {
            var shema = new Shema();

            var shemaLayer = new ShemaLayer();
            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.Black,
                Level = 0
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x48, 0x3d, 0x8b),
                Level = 640
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x84, 0x70, 0xff),
                Level = 675
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0xee, 0xdd, 0x82),
                Level = 770
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x6b, 0x8e, 0x23),
                Level = 945
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.Black,
                Level = 1275
            });
            shema.Layers.Add(shemaLayer);

            var engine = new GenerationEngine();

            var area = new GenerationArea(500, 500);

            var result = engine.Generate(shema, area).Colors;

            using (var image = new Image<Rgba32>(area.Rect.Width, area.Rect.Height))
            {
                for (var x = 0; x < area.Rect.Width; x++)
                for (var y = 0; y < area.Rect.Height; y++)
                {
                    var c = result[x, y];
                    image[x, y] = new Rgba32(c.R, c.G, c.B, c.A);
                }

                image.Save("bar.jpg");
            }

            File.WriteAllText("planet.json", JsonConvert.SerializeObject(shema, Formatting.Indented));
            shema = JsonConvert.DeserializeObject<Shema>(File.ReadAllText("planet.json"));
        }
    }
}