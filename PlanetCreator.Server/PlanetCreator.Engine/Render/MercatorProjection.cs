using System;
using PlanetCreator.Engine.Primitives;

namespace PlanetCreator.Engine.Render
{
    public class MercatorProjection : ProjectionBase
    {
        private readonly int _k;

        public override Point3D GetLocation3D(int x, int y)
        {
            var yk = Math.PI * (2.0 * (y - _k) - FullHeight) / FullWidth;
            yk = Math.Exp(2 * yk);
            yk = (yk - 1) / (yk + 1);
            var scale1 = FullWidth / (double) FullHeight / Math.Sqrt(1.0 - yk * yk) / Math.PI;
            var cos2 = Math.Sqrt(1.0 - yk * yk);

            var theta1 = Lng - 0.5 * Math.PI + Math.PI * (2.0 * x - FullWidth) / FullWidth;
            return new Point3D
            {
                X = Math.Cos(theta1) * cos2,
                Y = yk,
                Z = -Math.Sin(theta1) * cos2
            };
        }

        public MercatorProjection(double lat, double lng, int fullWidth, int fullHeight)
            : base(lat, lng, fullWidth, fullHeight)
        {
            var y = Math.Sin(lat);
            y = (1.0 + y) / (1.0 - y);
            y = 0.5 * Math.Log(y);
            _k = (int) (0.5 * y * fullWidth / Math.PI);
        }
    }
}