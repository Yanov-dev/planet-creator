using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using planet_craator.engine;
using planet_creator.engine.extensions;
using planet_creator.server.dto;

namespace planet_creator.server.Controllers
{
    [Route("api/planet/2d")]
    public class Planet2dController : Controller
    {
        [HttpPost("generate")]
        public async Task<byte[]> Generate([FromBody] Generate2DRequest request)
        {
            return await Task.Run(() =>
            {
                var engine = new GenerationEngine();

                var planet = engine.Generate(request.Shema, request.Area);

                var ms = new MemoryStream();

                planet.ToPng(ms);

                return ms.ToArray();
            });
        }

        [HttpPost("layer_preview")]
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