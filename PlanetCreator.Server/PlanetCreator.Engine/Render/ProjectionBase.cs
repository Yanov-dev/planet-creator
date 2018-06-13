using PlanetCreator.Engine.Primitives;

namespace PlanetCreator.Engine.Render
{
    public abstract class ProjectionBase
    {
        public double Lat { get; }
        public double Lng { get; }
        public int FullWidth { get; }
        public int FullHeight { get; }

        public ProjectionBase(double lat, double lng, int fullWidth, int fullHeight)
        {
            Lat = lat;
            Lng = lng;
            FullWidth = fullWidth;
            FullHeight = fullHeight;
        }

        public abstract Point3D GetLocation3D(int x, int y);

      
    }
}