using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace planet_craator.engine
{
    public class ShemaLayer
    {
        public List<ColorLevel> Colors { get; set; }

        [JsonIgnore]
        public int MinLevel => Colors.Min(e => e.Level);

        [JsonIgnore]
        public int MaxLevel => Colors.Max(e => e.Level);

        public bool IsEnable { get; set; }

        public double Seed { get; set; }

        public ShemaLayer()
        {
            Colors = new List<ColorLevel>();
        }
    }
}