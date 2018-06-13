using System;

namespace PlanetCreator.Engine
{
    public struct Range
    {
        public double Min { get; }

        public double Max { get; }

        public Range(double min, double max)
        {
            if (max <= min)
                throw new ArgumentOutOfRangeException();

            Min = min;
            Max = max;
        }

        public double Scale(double val, Range otherRange)
        {
            var range = Max - Min;

            var percent = (val - Min) / range;

            var newRange = otherRange.Max - otherRange.Min;

            return otherRange.Min + newRange * percent;
        }
    }
}