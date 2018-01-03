using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using planet_craator.engine.Extionsions;

namespace planet_craator.engine
{
    public class ColorContainer
    {
        private class ColorChank
        {
            public int MinLevel { get; set; }

            public int MaxLevel { get; set; }

            public Color MinColor { get; set; }

            public Color MaxColor { get; set; }

            public int Range => MaxLevel - MinLevel;
        }

        private readonly List<ColorChank> _chanks;

        public int MinLevel => _chanks.Min(e => e.MinLevel);

        public int MaxLevel => _chanks.Max(e => e.MaxLevel);

        public ColorContainer(ShemaLayer layer)
        {
            _chanks = new List<ColorChank>();

            var sorted = layer.Colors.OrderBy(e => e.Level).ToList();
            for (var i = 0; i < sorted.Count - 1; i++)
            {
                var chank = new ColorChank
                {
                    MinLevel = sorted[i].Level,
                    MaxLevel = sorted[i + 1].Level,
                    MinColor = sorted[i].Color,
                    MaxColor = sorted[i + 1].Color
                };

                _chanks.Add(chank);
            }
        }

        public Color GetColor(double level)
        {
            var chank = _chanks.FirstOrDefault(e => e.MinLevel <= level && e.MaxLevel >= level);
            if (chank == null)
                return Color.Black;

            var percent = (level - chank.MinLevel) / chank.Range;

            return chank.MinColor.Lerp(chank.MaxColor, percent);
        }
    }
}