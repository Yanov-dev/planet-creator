using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using planet_craator.engine;
using planet_craator.engine.GenerationAlgorithm;
using planet_craator.engine.Primitives;
using SixLabors.ImageSharp;

namespace planet_creator.engine3D
{
    public class Planet3dGenerator
    {
        private readonly int _recursionLevel;
        private readonly int _recursionLevelImage;
        private readonly double _seed;
        private ColorBitmapScheme _colorBitmapScheme;

        public Planet3dGenerator(int recursionLevel, int recursionLevelImage, double seed)
        {
            if (recursionLevelImage < _recursionLevel)
                throw new Exception("recursion level of image must me equels or more the recuresion level");

            _recursionLevel = recursionLevel;
            _recursionLevelImage = recursionLevelImage;
            _seed = seed;
        }

        private SphereModel GetSkeletone(int recursionLevel)
        {
            var basePoints = GetBaseGeodesicPoints();
            var model = new SphereModel(basePoints);

            var rootTriengles = GetBaseSkeleton(model);

            foreach (var tringle in rootTriengles)
                tringle.GenerateChildren(recursionLevel);

            model.Normalize();
            model.RootTriengles = rootTriengles;

            return model;
        }

        public void Generate()
        {
            _colorBitmapScheme = CreateColorBitmap();
        }

        public void Export(ColorContainer colorContainer, Stream imageStream, Stream objStream)
        {
            var widht = _colorBitmapScheme.Width;
            var height = _colorBitmapScheme.Height;

            var algorithm = new DefaultGenerationAlgorithm(_seed);

            if (imageStream != null)
                using (var image = new Image<Rgba32>(widht, height))
                {
                    var range = algorithm.GetRange();

                    var colorRange = colorContainer.Range;

                    for (var x = 0; x < widht; x++)
                    for (var y = 0; y < height; y++)
                    {
                        var p3 = _colorBitmapScheme.Points[x, y];
                        var alt = algorithm.GetAlt(p3, 50);

                        alt = range.Scale(alt, colorRange);

                        var c = colorContainer.GetColor(alt);

                        image[x, y] = new Rgba32(c.R, c.G, c.B, c.A);
                    }

                    image.SaveAsPng(imageStream);
                }

            using (var sw = new StreamWriter(objStream))
            {
                for (var i = 0; i < _colorBitmapScheme.SphereModel.Points.Count; i++)
                {
                    var point = _colorBitmapScheme.SphereModel.Points[i];
//
//                    point *= 1 + algorithm.GetAlt(point, 50);

                    sw.WriteLine($"v {point.X} {point.Y} {point.Z}");
                }

                var triengles = _colorBitmapScheme
                    .SphereModel
                    .GetAllTriengles()
                    .Where(e => e.Level == _recursionLevel)
                    .ToList();

                foreach (var t in triengles)
                {
                    sw.WriteLine($"vt {t.Vts[0].X / (double) widht} {1 - t.Vts[0].Y / (double) height}");
                    sw.WriteLine($"vt {t.Vts[1].X / (double) widht} {1 - t.Vts[1].Y / (double) height}");
                    sw.WriteLine($"vt {t.Vts[2].X / (double) widht} {1 - t.Vts[2].Y / (double) height}");
                }

                sw.WriteLine();

                for (var index = 0; index < triengles.Count; index++)
                {
                    var triengle = triengles[index];
                    sw.WriteLine(
                        $"f {triengle.Points[0] + 1}/{index * 3 + 1} {triengle.Points[1] + 1}/{index * 3 + 2} {triengle.Points[2] + 1}/{index * 3 + 3}");
                }
            }

            objStream?.Close();
            imageStream?.Close();
        }

        private IEnumerable<int> GetCommonPoints(RefTriengle t1, RefTriengle t2)
        {
            foreach (var point in t1.Points)
                if (t2.Points.Any(e => e == point))
                    yield return point;
        }

        private ColorBitmapScheme CreateColorBitmap()
        {
            var len = (int) Math.Pow(2, _recursionLevelImage) + 1;
            var widht = len * 5;
            var height = len * 2;

            var imageCords = new Point3D[widht, height];

            var relations = new List<(int left, int right)>
            {
                (13, 12),
                (8, 3),
                (16, 6),
                (2, 1),
                (0, 4),
                (9, 18),
                (17, 7),
                (10, 11),
                (15, 5),
                (19, 14)
            };

            var colorSphere = GetSkeletone(_recursionLevelImage);

            for (var index = 0; index < 10; index++)
            {
                var pair = relations[index];

                var leftT = colorSphere.RootTriengles[pair.left];
                var rightT = colorSphere.RootTriengles[pair.right];

                leftT.GenerateChildren(_recursionLevelImage);
                rightT.GenerateChildren(_recursionLevelImage);

                var offsetX = len * (index % 5);
                var offsetY = len * (index / 5);

                var p = new Point2(offsetX, offsetY);
                var pX = new Point2(offsetX + len - 1, offsetY);
                var pY = new Point2(offsetX, offsetY + len - 1);
                var pXY = new Point2(offsetX + len - 1, offsetY + len - 1);

                var commonId = GetCommonPoints(leftT, rightT).ToList();

                var d = new Dictionary<int, Point2>
                {
                    [commonId[0]] = p,
                    [commonId[1]] = pXY,
                    [leftT.Points.First(e => !commonId.Contains(e))] = pX,
                    [rightT.Points.First(e => !commonId.Contains(e))] = pY
                };

                var t1 = new Triengle2To3(leftT, leftT.Points.Select(e => d[e]).ToArray());
                var t2 = new Triengle2To3(rightT, rightT.Points.Select(e => d[e]).ToArray());

                var children = t1
                    .GetChildren(_recursionLevelImage)
                    .Concat(t2.GetChildren(_recursionLevelImage))
                    .ToList();

                foreach (var child in children)
                    for (var i = 0; i < child.Points.Length; i++)
                    {
                        var p2 = child.Points[i];
                        var p3 = colorSphere.Points[child.Triengle.Points[i]];
                        imageCords[p2.X, p2.Y] = p3;
                    }
            }

            var sphereModel = GetSkeletone(_recursionLevel);

            var colorT = colorSphere.GetAllTriengles().Where(e => e.Level == _recursionLevel).ToList();
            var triengles = sphereModel.GetAllTriengles().Where(e => e.Level == _recursionLevel).ToList();
            for (int i = 0; i < colorT.Count; i++)
            {
                var ct = colorT[i];
                for (int vti = 0; vti < ct.Vts.Length; vti++)
                {
                    triengles[i].Vts[vti] = ct.Vts[vti];
                }
            }

            return new ColorBitmapScheme(widht, height, imageCords, sphereModel);
        }

        private List<RefTriengle> GetBaseSkeleton(SphereModel sphereModel)
        {
            var triengles = new List<RefTriengle>();

            triengles.Add(new RefTriengle(0, 11, 5, 0, sphereModel));
            triengles.Add(new RefTriengle(0, 5, 1, 0, sphereModel));
            triengles.Add(new RefTriengle(0, 1, 7, 0, sphereModel));
            triengles.Add(new RefTriengle(0, 7, 10, 0, sphereModel));
            triengles.Add(new RefTriengle(0, 10, 11, 0, sphereModel));

            // 5 adjacent faces 
            triengles.Add(new RefTriengle(1, 5, 9, 0, sphereModel));
            triengles.Add(new RefTriengle(5, 11, 4, 0, sphereModel));
            triengles.Add(new RefTriengle(11, 10, 2, 0, sphereModel));
            triengles.Add(new RefTriengle(10, 7, 6, 0, sphereModel));
            triengles.Add(new RefTriengle(7, 1, 8, 0, sphereModel));

            // 5 faces around point 3
            triengles.Add(new RefTriengle(3, 9, 4, 0, sphereModel));
            triengles.Add(new RefTriengle(3, 4, 2, 0, sphereModel));
            triengles.Add(new RefTriengle(3, 2, 6, 0, sphereModel));
            triengles.Add(new RefTriengle(3, 6, 8, 0, sphereModel));
            triengles.Add(new RefTriengle(3, 8, 9, 0, sphereModel));

            // 5 adjacent faces 
            triengles.Add(new RefTriengle(4, 9, 5, 0, sphereModel));
            triengles.Add(new RefTriengle(2, 4, 11, 0, sphereModel));
            triengles.Add(new RefTriengle(6, 2, 10, 0, sphereModel));
            triengles.Add(new RefTriengle(8, 6, 7, 0, sphereModel));
            triengles.Add(new RefTriengle(9, 8, 1, 0, sphereModel));

            return triengles;
        }

        private List<Point3D> GetBaseGeodesicPoints()
        {
            var t = (1.0 + Math.Sqrt(5.0)) / 2.0;
            var bw = new List<Point3D>
            {
                new Point3D(-1, t, 0),
                new Point3D(1, t, 0),
                new Point3D(-1, -t, 0),
                new Point3D(1, -t, 0),

                new Point3D(0, -1, t),
                new Point3D(0, 1, t),
                new Point3D(0, -1, -t),
                new Point3D(0, 1, -t),

                new Point3D(t, 0, -1),
                new Point3D(t, 0, 1),
                new Point3D(-t, 0, -1),
                new Point3D(-t, 0, 1)
            };

            return bw;
        }
    }
}