using System;

namespace planet_craator.engine.Primitives
{
    public struct Point3D
    {
        public double X;
        public double Y;
        public double Z;
        
        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public void Normalize()
        {
            var len = Math.Sqrt(X * X + Y * Y + Z * Z);
            X /= len;
            Y /= len;
            Z /= len;
        }
        
        public static Point3D operator *(Point3D p, double len)
        {
            return new Point3D(p.X * len, p.Y * len, p.Z * len);
        }

        public static Point3D operator /(Point3D p, double value)
        {
            return p * (1 / value);
        }

        public static Point3D operator +(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3D operator -(Point3D p1, Point3D p2)
        {
            return new Point3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public double DistanceTo(Point3D other)
        {
            return (other - this).Length;
        }
    }
}