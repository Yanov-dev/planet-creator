using planet_craator.engine;

namespace planet_creator.server.dto
{
    public class Generate2DRequest
    {
        public Shema Shema { get; set; }
        
        public GenerationArea Area { get; set; }
    }
}