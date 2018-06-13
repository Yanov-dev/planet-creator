using System.Drawing;

namespace PlanetCreator.Engine
{
    public class Planet2D
    {
        public Color[,] Colors { get; }
        
        public int Width { get; }
        
        public int Height { get; }

        public Planet2D(Color[,] colors, int width, int height)
        {
            Colors = colors;
            Width = width;
            Height = height;
        }
    }
}