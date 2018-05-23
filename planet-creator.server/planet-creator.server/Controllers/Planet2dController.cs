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

       
    }
}