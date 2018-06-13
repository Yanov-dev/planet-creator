using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanetCreator.Engine;
using PlanetCreator.Engine3D;
using PlanetCreator.Server.Dto;

namespace PlanetCreator.Server.Controllers
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
        public async Task<Generation3dResults> Generate([FromBody] Generate3dRequest request)
        {
            var gen = new Planet3dGenerator(request.ObjDepth, request.ImageDepth, request.Seed);

            var objId = Guid.NewGuid();
            var imageId = Guid.NewGuid();

            gen.Generate();

            if (!Directory.Exists("Files"))
                Directory.CreateDirectory("Files");

            //var colorContainer = new ColorContainer(GetShema().Layers[0]);
            var colorContainer = new ColorContainer(request.Layer);
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

        private ShemaLayer GetRandom()
        {
            var rand = new Random();

            var levels = rand.Next(4, 7);

            var shemaLayer = new ShemaLayer();

            var offset = 0;
            for (int i = 0; i < levels; i++)
            {
                var height = rand.Next(50, 400);

                shemaLayer.Colors.Add(new ColorLevel
                {
                    Level = offset + height,
                    Color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255))
                });

                offset += height;
            }

            return shemaLayer;
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