using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using planet_craator.engine;
using planet_creator.engine3D;
using planet_creator.server.Models;

namespace planet_creator.server.Controllers
{
    [Route("api/planet/3d")]
    public class Planet3dController : Controller
    {
        [HttpGet("{file}")]
        public async Task<Stream> Take(string file)
        {
            return System.IO.File.OpenRead(Path.Combine("Files", file));
        }

        [HttpPost("generate")]
        public async Task<Generation3dResults> Generate()
        {
            var gen = new Planet3dGenerator(2, 5);

            var objId = Guid.NewGuid();
            var imageId = Guid.NewGuid();

            gen.Generate();

            if (!Directory.Exists("Files"))
                Directory.CreateDirectory("Files");

            var colorContainer = new ColorContainer(GetShema().Layers[0]);
            var objName = $"{objId}.obj";
            var imageName = $"{imageId}.png";
            gen.Export(
                colorContainer,
                System.IO.File.Create(Path.Combine("Files", imageName)),
                System.IO.File.Create(Path.Combine("Files", objName))
            );

            return new Generation3dResults
            {
                Obj = objName,
                Image = imageName
            };
        }

        private Shema GetShema()
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

            return shema;
        }
    }
}