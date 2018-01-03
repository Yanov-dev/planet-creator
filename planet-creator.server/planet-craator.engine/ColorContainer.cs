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
            public int MinAlt { get; set; }

            public int MaxAlt { get; set; }

            public Color MinColor { get; set; }

            public Color MaxColor { get; set; }

            public int Range => MaxAlt - MinAlt;
        }

        private readonly List<ColorChank> _chanks;

        public ColorContainer(ShemaLayer layer)
        {
            _chanks = new List<ColorChank>();

            var sorted = layer.Colors.OrderBy(e => e.Level).ToList();
            for (var i = 0; i < sorted.Count - 1; i++)
            {
                var chank = new ColorChank
                {
                    MinAlt = sorted[i].Level,
                    MaxAlt = sorted[i + 1].Level,
                    MinColor = sorted[i].Color,
                    MaxColor = sorted[i + 1].Color
                };

                _chanks.Add(chank);
            }
        }

        public Color GetColor(double alt)
        {
            var chank = _chanks.FirstOrDefault(e => e.MinAlt <= alt && e.MaxAlt >= alt);
            if (chank == null)
                return Color.Black;

            var percent = (alt - chank.MinAlt) / chank.Range;

            return chank.MinColor.Lerp(chank.MaxColor, percent);
        }
    }
}