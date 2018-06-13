using PlanetCreator.Engine.Primitives;

namespace PlanetCreator.Engine3D
{
    public class ColorBitmapScheme
    {
        public ColorBitmapScheme(int width, int height, Point3D[,] points, SphereModel sphereModel)
        {
            Points = points;
            SphereModel = sphereModel;
            Height = height;
            Width = width;
        }

        public int Width { get; }

        public int Height { get; }

        public Point3D[,] Points { get; }
        
        public SphereModel SphereModel { get; }
    }
}