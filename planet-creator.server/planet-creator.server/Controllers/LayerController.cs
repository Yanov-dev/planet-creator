using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using planet_craator.engine;
using planet_creator.engine.extensions;

namespace planet_creator.server.Controllers
{
    [Route("api/layer")]
    public class LayerController : Controller
    {
        [HttpGet]
        public ShemaLayer Get()
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

        [HttpPost("preview")]
        public async Task<byte[]> LayerPreview([FromBody] ShemaLayer layer)
        {
            return await Task.Run(() =>
            {
                var cc = new ColorContainer(layer);

                var ms = new MemoryStream();

                cc.GeneratePreview(new Size(100, 500), ms);
                return ms.ToArray();
            });
        }
    }
}