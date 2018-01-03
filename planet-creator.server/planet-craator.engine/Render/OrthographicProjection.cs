using System;
using planet_craator.engine.Primitives;

namespace planet_craator.engine.Render
{
    public class OrthographicProjection : ProjectionBase
    {
        private double _sLat;
        private double _cLat;
        private double _sLng;
        private double _cLng;

        public OrthographicProjection(double lat, double lng, int fullWidth, int fullHeight)
            : base(lat, lng, fullWidth, fullHeight)
        {
            _sLat = Math.Sin(lat);
            _cLat = Math.Cos(lat);
            _sLng = Math.Sin(lng);
            _cLng = Math.Cos(lng);
        }

        public override Point3D GetLocation3D(int x, int y)
        {
            var ymin = -2.0;
            var ymax = 2.0;

            var lx = (2.0 * x - FullWidth) / FullHeight;
            var ly = (2.0 * y - FullHeight) / FullHeight;
            if (lx * lx + ly * ly > 1.0)
                return new Point3D();
            
            var z = Math.Sqrt(1.0 - lx * lx - ly * ly);
            var x1 = _cLng * lx + _sLng * _sLat * ly + _sLng * _cLat * z;
            var y1 = _cLat * ly - _sLat * z;
            var z1 = -_sLng * lx + _cLng * _sLat * ly + _cLng * _cLat * z;
            if (y1 < ymin)
                ymin = y1;
            if (y1 > ymax)
                ymax = y1;

            return new Point3D
            {
                X = x1,
                Y = y1,
                Z = z1
            };
        }
    }
}