using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using planet_craator.engine.Primitives;

namespace planet_creator.engine3D
{
    public class SphereModel
    {
        public List<Point3D> Points { get; private set; }

        public List<RefTriengle> RootTriengles { get; set; }

        public List<Color> Colors { get; private set; }

        public List<RefTriengle> GetAllTriengles()
        {
            return RootTriengles.SelectMany(e => e.GetTree()).ToList();
        }

        private readonly SpherePoints<int> __Cache;

        public SpherePoints<int> Cache
        {
            get { return __Cache; }
        }

        public SphereModel(List<Point3D> points)
        {
            __Cache = new SpherePoints<int>();

            Points = new List<Point3D>();
            Points.AddRange(points);

            Colors = new List<Color>();

            foreach (var f in Points)
                Colors.Add(getDefaultColor());
        }

        private Color getDefaultColor()
        {
            return Color.FromArgb(0x80, 0x80, 0x80);
        }

        public int MiddlePoint(int p1, int p2)
        {
            int resIndex;

            if (__Cache.Get(p1, p2, out resIndex))
                return resIndex;

            var res = Points[p1] + Points[p2];

            res.Normalize();

            resIndex = Points.Count;

            Points.Add(res);
            Colors.Add(getDefaultColor());

            __Cache.Add(p1, p2, resIndex);

            return resIndex;
        }

        public void Normalize()
        {
            for (int index = 0; index < Points.Count; index++)
            {
                var point = Points[index];
                point.Normalize();

                Points[index] = point;
            }
        }

        /// <summary>
        /// Method for optimize algorithm
        /// </summary>
        public void RemoveUselessPoints()
        {
            var pointIndexes = RootTriengles.SelectMany(e => e.Points).Distinct().ToList();

            Points = pointIndexes.Select(e => Points[e]).ToList();
            Colors = pointIndexes.Select(e => Colors[e]).ToList();

            foreach (var t in RootTriengles)
            {
                for (int i = 0; i < t.Points.Length; i++)
                {
                    var pointIndex = t.Points[i];

                    var newIndex = pointIndexes.IndexOf(pointIndex);

                    t.Points[i] = newIndex;
                }
            }
        }
    }
}