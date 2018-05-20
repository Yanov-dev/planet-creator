using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using planet_craator.engine;
using planet_creator.engine.extensions;
using planet_creator.engine3D;

namespace planet_creator.engine.benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new Planet3dGenerator(5, 10);

//            var finder = new Finder();
//
//            var res = finder.Find(model.RootTriengles);
//
//            foreach (var v in res)
//            {
//                Console.WriteLine($"{v.left.Id} - {v.right.Id}");
//            }

            //model.ExportToObj(File.OpenWrite("planet.obj"));

            var sw = Stopwatch.StartNew();
            
            gen.Generate();
            
            Console.WriteLine(sw.Elapsed);

            var colorContainer = new ColorContainer(GetShema().Layers[0]);
            var imageStream = File.Open(@"C:\AllFiles\testAppEmpty\src\assets\image.png", FileMode.Create);
            var objStream = File.Open(@"C:\AllFiles\testAppEmpty\src\assets\planet.obj", FileMode.Create);
            
            sw.Restart();
            gen.Export(colorContainer, imageStream, objStream);
            //gen.Export(colorContainer, null, objStream);
            Console.WriteLine(sw.Elapsed);

            //SingleThreadTest();

            //FourThreadsTest();

            //ColorContainerTest();
        }

        private static void ColorContainerTest()
        {
            var cc = new ColorContainer(GetShema().Layers[0]);
            var count = 1000 * 1000;
            var colors = new Color[count];

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                colors[i] = cc.GetColor(0);
            }

            sw.Stop();
            Console.WriteLine($"ColorContainer - {sw.ElapsedMilliseconds}ms");
        }

        private static void FourThreadsTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var tasks = new Task[4];
            tasks[0] = Task.Run(() => { RenderArea(new GenerationArea(1000, 1000, 0, 0, 500, 500)); });
            tasks[1] = Task.Run(() => { RenderArea(new GenerationArea(1000, 1000, 0, 500, 500, 500)); });
            tasks[2] = Task.Run(() => { RenderArea(new GenerationArea(1000, 1000, 500, 0, 500, 500)); });
            tasks[3] = Task.Run(() => { RenderArea(new GenerationArea(1000, 1000, 500, 500, 500, 500)); });
            Task.WaitAll(tasks);
            sw.Stop();

            Console.WriteLine($"Four threads time - {sw.ElapsedMilliseconds}ms");
        }

        private static void SingleThreadTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            RenderArea(new GenerationArea(1000, 1000));
            sw.Stop();

            Console.WriteLine($"Single thread time - {sw.ElapsedMilliseconds}ms");
        }

        private static void RenderArea(GenerationArea area)
        {
            var engine = new GenerationEngine();

            var result = engine.Generate(GetShema(), area);
        }

        private static Shema GetShema()
        {
            var shema = new Shema();

            var shemaLayer = new ShemaLayer();
            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.Black,
                Level = 0
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x48, 0x3d, 0x8b),
                Level = 640
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x84, 0x70, 0xff),
                Level = 675
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0xee, 0xdd, 0x82),
                Level = 770
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.FromArgb(0x6b, 0x8e, 0x23),
                Level = 945
            });

            shemaLayer.Colors.Add(new ColorLevel
            {
                Color = Color.Black,
                Level = 1275
            });
            shema.Layers.Add(shemaLayer);

            return shema;
        }
    }
}