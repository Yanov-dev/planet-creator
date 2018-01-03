using System.Drawing;
using Newtonsoft.Json;
using planet_craator.engine.Converters;

namespace planet_craator.engine
{
    public class ColorLevel
    {
        public int Level { get; set; }
        
        [JsonConverter(typeof(JsonColorConverter))]
        public Color Color { get; set; }
    }
}