using System.Collections.Generic;
using System.Linq;
using PlanetCreator.Engine.Primitives;

namespace PlanetCreator.Engine3D
{
    public class RefTriengle
    {
        private readonly SphereModel _sphereModel;
        public int[] Points { get; }

        public Point2[] Vts { get; }

        public RefTriengle Parent { get; }

        public RefTriengle[] Children { get; }

        public int Level { get; }
        
        public RefTriengle(int p1, int p2, int p3, int level, SphereModel sphereModel)
        {
            _sphereModel = sphereModel;
            Points = new[] {p1, p2, p3};
            Level = level;

            Vts = new Point2[3];

            Children = new RefTriengle[4];
        }

        public RefTriengle Clone()
        {
            return new RefTriengle(Points[0], Points[1], Points[2], Level, _sphereModel);
        }

        public void GenerateChildren(int depth)
        {
            if (depth == 0)
                return;

            var a = _sphereModel.MiddlePoint(Points[0], Points[1]);
            var b = _sphereModel.MiddlePoint(Points[1], Points[2]);
            var c = _sphereModel.MiddlePoint(Points[2], Points[0]);

            Children[0] = new RefTriengle(Points[0], a, c, Level + 1, _sphereModel);
            Children[1] = new RefTriengle(Points[1], b, a, Level + 1, _sphereModel);
            Children[2] = new RefTriengle(Points[2], c, b, Level + 1, _sphereModel);
            Children[3] = new RefTriengle(a, b, c, Level + 1, _sphereModel);

            foreach (var child in Children)
                child.GenerateChildren(depth - 1);
        }

        public List<Point3D> GetPoint3F()
        {
            return Points.Select(e => _sphereModel.Points[e]).ToList();
        }

        public IEnumerable<RefTriengle> GetTree()
        {
            yield return this;

            foreach (var child in Children.Where(e => e != null))
            foreach (var triengle in child.GetTree())
                yield return triengle;
        }
    }
}