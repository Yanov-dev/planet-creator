using System;
using System.Collections.Generic;
using System.Linq;

namespace planet_creator.engine3D
{
    public class Finder
    {
        private readonly Random _rand;

        public Finder()
        {
            _rand = new Random();
        }


        public List<(int left, int right)> Find(List<RefTriengle> triengles)
        {
            for (int i = 0; i < 10000; i++)
            {
                var res = FindPairs(triengles);
                if (res != null)
                {
                    return res.Select(e =>
                    {
                        return (triengles.IndexOf(e.left), triengles.IndexOf(e.right));
                    }).ToList();
                }
            }

            return null;
        }

        private List<(RefTriengle left, RefTriengle right)> FindPairs(List<RefTriengle> triengles)
        {
            var list = new List<RefTriengle>(triengles);

            var pairs = new List<(RefTriengle left, RefTriengle right)>();

            while (list.Count > 0)
            {
                var t = list[_rand.Next(list.Count)];

                list.Remove(t);

                var right = FindPair(t, list);
                if (right == null)
                    return null;

                pairs.Add((t, right));

                list.Remove(right);
            }

            return pairs;
        }

        private RefTriengle FindPair(RefTriengle left, List<RefTriengle> list)
        {
            foreach (var t in list)
            {
                var count = 0;

                foreach (var point in t.Points)
                {
                    if (left.Points.Any(e => e == point))
                        count++;
                }

                if (count == 2)
                    return t;
            }

            return null;
        }
    }
}