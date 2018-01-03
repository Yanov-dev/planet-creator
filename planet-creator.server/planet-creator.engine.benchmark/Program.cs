using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using planet_craator.engine;

namespace planet_creator.engine.benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleThreadTest();

            FourThreadsTest();
            
            Console.ReadKey();
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