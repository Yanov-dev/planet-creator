using System.Drawing;
using Newtonsoft.Json;
using PlanetCreator.Engine.Converters;

namespace PlanetCreator.Engine
{
    public class GenerationArea
    {
        public int FullWidth { get; set; }

        public int FullHeight { get; set; }

        [JsonConverter(typeof(JsonRectangleConverter))]
        public Rectangle Rect { get; set; }

        public GenerationArea()
        {
        }

        public GenerationArea(int fullWidth, int fullHeight)
        {
            FullWidth = fullWidth;
            FullHeight = fullHeight;
            Rect = new Rectangle(0, 0, fullWidth, fullHeight);
        }

        public GenerationArea(int fullWidth, int fullHeight, int x, int y, int width, int height)
        {
            FullWidth = fullWidth;
            FullHeight = fullHeight;
            Rect = new Rectangle(x, y, width, height);
        }
    }
}