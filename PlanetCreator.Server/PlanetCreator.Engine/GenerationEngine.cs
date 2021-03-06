﻿using System.ComponentModel;
using System.Drawing;
using PlanetCreator.Engine.GenerationAlgorithm;
using PlanetCreator.Engine.Render;

namespace PlanetCreator.Engine
{
    public class GenerationEngine
    {
        public Planet2D Generate(
            Shema shema,
            GenerationArea area
        )
        {
            return RenderArea(shema, shema.Layers[0], area);
        }

        private Planet2D RenderArea(
            Shema shema,
            ShemaLayer layer,
            GenerationArea area)
        {
            var projection = GetProjection(shema.ProjectionType, shema.Lat, shema.Lng, area.FullWidth, area.FullHeight);
            var algorithm = new DefaultGenerationAlgorithm(layer.Seed);

            var range = algorithm.GetRange();
            var colorContainer = new ColorContainer(layer);

            var colorRange = colorContainer.Range;

            var bmp = new Color[area.Rect.Width, area.Rect.Height];

            for (var x = area.Rect.X; x < area.Rect.X + area.Rect.Width; x++)
            {
                for (var y = area.Rect.Y; y < area.Rect.Y + area.Rect.Height; y++)
                {
                    var p3 = projection.GetLocation3D(x, y);

                    var alt = algorithm.GetAlt(p3, 50);

                    alt = range.Scale(alt, colorRange);

                    var color = colorContainer.GetColor(alt);

                    bmp[x - area.Rect.X, y - area.Rect.Y] = color;
                }
            }

            return new Planet2D(bmp, area.Rect.Width, area.Rect.Height);
        }

        private ProjectionBase GetProjection(ProjectionType type, double lat, double lng, int fullWidth, int fullHeight)
        {
            switch (type)
            {
                case ProjectionType.Mercator:
                    return new MercatorProjection(lat, lng, fullWidth, fullHeight);
                case ProjectionType.Orthographic:
                    return new OrthographicProjection(lat, lng, fullWidth, fullHeight);
                default:
                    throw new InvalidEnumArgumentException(nameof(type));
            }
        }
    }
}