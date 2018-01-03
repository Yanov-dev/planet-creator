using System.Diagnostics;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using planet_craator.engine;
using planet_creator.engine.extensions;
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

            var cc = new ColorContainer(shemaLayer);

            cc.GeneratePreview(new Size(100, 500), File.OpenWrite("priview.png"));

            var engine = new GenerationEngine();

            var area = new GenerationArea(500, 500);

            var planet = engine.Generate(shema, area);

            planet.ToPng(File.OpenWrite("planet.png"));

            File.WriteAllText("planet.json", JsonConvert.SerializeObject(shema, Formatting.Indented));
            shema = JsonConvert.DeserializeObject<Shema>(File.ReadAllText("planet.json"));
        }
    }
}