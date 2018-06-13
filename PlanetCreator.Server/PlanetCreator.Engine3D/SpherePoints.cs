using System.Collections.Generic;
using System.Drawing;

namespace PlanetCreator.Engine3D
{
    public class SpherePoints<T>
    {
        private readonly Dictionary<Point, T> __Cache;

        public SpherePoints()
        {
            __Cache = new Dictionary<Point, T>();
        }

        public bool Get(int index1, int index2, out T t)
        {
            var ap = new Point(index1, index2);
            var revAp = new Point(index2, index1);

            if (__Cache.ContainsKey(ap))
            {
                t = __Cache[ap];
                return true;
            }

            if (__Cache.ContainsKey(revAp))
            {
                t = __Cache[revAp];
                return true;
            }

            t = default(T);
            return false;
        }

        public void Add(int index1, int index2, T t)
        {
            __Cache[new Point(index1, index2)] = t;
        }
    }
}