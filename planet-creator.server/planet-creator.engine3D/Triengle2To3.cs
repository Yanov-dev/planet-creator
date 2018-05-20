using System.Collections.Generic;
using System.Drawing;

namespace planet_creator.engine3D
{
    public class Triengle2To3
    {
        public RefTriengle Triengle { get; }
        public Point2[] Points { get; }

        public Triengle2To3(RefTriengle triengle, params Point2[] points)
        {
            Triengle = triengle;
            Points = points;
        }

        public IEnumerable<Triengle2To3> GetChildren(int depth)
        {
            for (var index = 0; index < Points.Length; index++)
            {
                var point = Points[index];
                Triengle.Vts[index] = point;
            }

            if (depth == 0)
            {
                yield return this;
                yield break;
            }

            var list = new List<Triengle2To3>();
          
            var a = Point2.MiddlePoint(Points[0], Points[1]);
            var b = Point2.MiddlePoint(Points[1], Points[2]);
            var c = Point2.MiddlePoint(Points[2], Points[0]);

            list.Add(new Triengle2To3(Triengle.Children[0], Points[0], a, c));
            list.Add(new Triengle2To3(Triengle.Children[1], Points[1], b, a));
            list.Add(new Triengle2To3(Triengle.Children[2], Points[2], c, b));
            list.Add(new Triengle2To3(Triengle.Children[3], a, b, c));

            foreach (var item in list)
            foreach (var child in item.GetChildren(depth - 1))
                yield return child;
        }
    }
}