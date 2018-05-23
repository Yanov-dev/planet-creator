using planet_craator.engine;

namespace planet_creator.server.Models
{
    public class Generate3dRequest
    {
        public ShemaLayer Layer { get; set; }
        
        public double Seed { get; set; }
        
        public int ImageDepth { get; set; }
        
        public int ObjDepth { get; set; }
    }
}