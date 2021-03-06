﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanetCreator.Engine;

namespace PlanetCreator.Controllers
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