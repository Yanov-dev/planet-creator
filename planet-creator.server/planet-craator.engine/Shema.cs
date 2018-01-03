using System.Collections.Generic;

namespace planet_craator.engine
{
    public class Shema
    {
        public List<ShemaLayer> Layers { get; set; }
        
        public double Lng { get; set; }
        
        public double Lat { get; set; }
        
        public double SeaLevel { get; set; }
        
        public ProjectionType ProjectionType { get; set; }

        public Shema()
        {
            Layers = new List<ShemaLayer>();
        }
    }
}