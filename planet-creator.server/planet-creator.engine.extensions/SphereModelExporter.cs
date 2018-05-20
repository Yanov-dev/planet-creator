using System.IO;
using planet_creator.engine3D;

namespace planet_creator.engine.extensions
{
    public static class SphereModelExporter
    {
        public static void ExportToObj(this SphereModel model, Stream stream)
        {
            using (var sw = new StreamWriter(stream))
            {
                for (int i = 0; i < model.Points.Count; i++)
                {
                    var point = model.Points[i];
                    var color = model.Colors[i];
                    sw.WriteLine($"v {point.X} {point.Y} {point.Z}");
                }

                sw.WriteLine();

                foreach (var triengle in model.RootTriengles)
                {
                    sw.WriteLine(string.Format("f {0} {1} {2}",
                        triengle.Points[0] + 1,
                        triengle.Points[1] + 1,
                        triengle.Points[2] + 1));
                }
            }
        }
    }
}