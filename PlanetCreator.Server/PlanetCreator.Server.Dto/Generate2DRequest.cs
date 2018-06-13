using PlanetCreator.Engine;

namespace PlanetCreator.Server.Dto
{
    public class Generate2DRequest
    {
        public Shema Shema { get; set; }
        
        public GenerationArea Area { get; set; }
    }
}