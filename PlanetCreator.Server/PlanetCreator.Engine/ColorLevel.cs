using System.Drawing;
using Newtonsoft.Json;
using PlanetCreator.Engine.Converters;

namespace PlanetCreator.Engine
{
    public class ColorLevel
    {
        public int Level { get; set; }
        
        [JsonConverter(typeof(JsonColorConverter))]
        public Color Color { get; set; }
    }
}