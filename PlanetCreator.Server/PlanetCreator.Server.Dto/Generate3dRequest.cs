using PlanetCreator.Engine;

namespace PlanetCreator.Server.Dto
{
    public class Generate3dRequest
    {
        public ShemaLayer Layer { get; set; }
        
        public double Seed { get; set; }
        
        public int ImageDepth { get; set; }
        
        public int ObjDepth { get; set; }
    }
}