using System;
using planet_craator.engine.Primitives;

namespace planet_craator.engine.GenerationAlgorithm
{
    public class DefaultGenerationAlgorithm : GenerationAlgorithmBase
    {
        private const double DD1 = 0.45; /* weight for altitude difference */
        private const double DD2 = 0.035; /* weight for distance */
        private const double POW = 0.47; /* power for distance function */

        private readonly double _rSeed1; /* seeds */
        private readonly double _rSeed2; /* seeds */
        private readonly double _rSeed3; /* seeds */
        private readonly double _rSeed4; /* seeds */

        double M = -.02; /* initial altitude (slightly below sea level) */

        public DefaultGenerationAlgorithm(double seed) : base(seed)
        {
            _rSeed1 = seed;

            _rSeed1 = rand2(_rSeed1, _rSeed1);
            _rSeed2 = rand2(_rSeed1, _rSeed1);
            _rSeed3 = rand2(_rSeed1, _rSeed2);
            _rSeed4 = rand2(_rSeed2, _rSeed3);
        }

        public override Range GetRange()
        {
            return new Range(-0.1, 0.1);
        }

        private double rand2(double p, double q)
        {
            double r = (p + 3.14159265) * (q + 3.14159265);
            return (2.0 * (r - (int) r) - 1.0);
        }

        public override double GetAlt(Point3D location, int depth)
        {
            return Planet1(location.X, location.Y, location.Z, depth);
        }

        private double ssa,
            ssb,
            ssc,
            ssd,
            ssas,
            ssbs,
            sscs,
            ssds,
            ssax,
            ssay,
            ssaz,
            ssbx,
            ssby,
            ssbz,
            sscx,
            sscy,
            sscz,
            ssdx,
            ssdy,
            ssdz;

        double Planet1(double x, double y, double z, int depth)
        {
            double abx, aby, abz, acx, acy, acz, adx, ady, adz, apx, apy, apz;
            double bax, bay, baz, bcx, bcy, bcz, bdx, bdy, bdz, bpx, bpy, bpz;

            abx = ssbx - ssax;
            aby = ssby - ssay;
            abz = ssbz - ssaz;
            acx = sscx - ssax;
            acy = sscy - ssay;
            acz = sscz - ssaz;
            adx = ssdx - ssax;
            ady = ssdy - ssay;
            adz = ssdz - ssaz;
            apx = x - ssax;
            apy = y - ssay;
            apz = z - ssaz;
            if ((adx * aby * acz + ady * abz * acx + adz * abx * acy
                 - adz * aby * acx - ady * abx * acz - adx * abz * acy) *
                (apx * aby * acz + apy * abz * acx + apz * abx * acy
                 - apz * aby * acx - apy * abx * acz - apx * abz * acy) > 0.0)
            {
                /* p is on same side of abc as d */
                if ((acx * aby * adz + acy * abz * adx + acz * abx * ady
                     - acz * aby * adx - acy * abx * adz - acx * abz * ady) *
                    (apx * aby * adz + apy * abz * adx + apz * abx * ady
                     - apz * aby * adx - apy * abx * adz - apx * abz * ady) > 0.0)
                {
                    /* p is on same side of abd as c */
                    if ((abx * ady * acz + aby * adz * acx + abz * adx * acy
                         - abz * ady * acx - aby * adx * acz - abx * adz * acy) *
                        (apx * ady * acz + apy * adz * acx + apz * adx * acy
                         - apz * ady * acx - apy * adx * acz - apx * adz * acy) > 0.0)
                    {
                        /* p is on same side of acd as b */
                        bax = -abx;
                        bay = -aby;
                        baz = -abz;
                        bcx = sscx - ssbx;
                        bcy = sscy - ssby;
                        bcz = sscz - ssbz;
                        bdx = ssdx - ssbx;
                        bdy = ssdy - ssby;
                        bdz = ssdz - ssbz;
                        bpx = x - ssbx;
                        bpy = y - ssby;
                        bpz = z - ssbz;
                        if ((bax * bcy * bdz + bay * bcz * bdx + baz * bcx * bdy
                             - baz * bcy * bdx - bay * bcx * bdz - bax * bcz * bdy) *
                            (bpx * bcy * bdz + bpy * bcz * bdx + bpz * bcx * bdy
                             - bpz * bcy * bdx - bpy * bcx * bdz - bpx * bcz * bdy) > 0.0)
                        {
                            /* p is on same side of bcd as a */
                            /* Hence, p is inside tetrahedron */
                            return (planet(ssa, ssb, ssc, ssd, ssas, ssbs, sscs, ssds,
                                ssax, ssay, ssaz, ssbx, ssby, ssbz,
                                sscx, sscy, sscz, ssdx, ssdy, ssdz,
                                x, y, z, 11));
                        }
                    }
                }
            } /* otherwise */

            return (planet(M, M, M, M,
                /* initial altitude is M on all corners of tetrahedron */
                _rSeed1, _rSeed2, _rSeed3, _rSeed4,
                /* same seed set is used in every call */
                -Math.Sqrt(3.0) - 0.20, -Math.Sqrt(3.0) - 0.22, -Math.Sqrt(3.0) - 0.23,
                -Math.Sqrt(3.0) - 0.19, Math.Sqrt(3.0) + 0.18, Math.Sqrt(3.0) + 0.17,
                Math.Sqrt(3.0) + 0.21, -Math.Sqrt(3.0) - 0.24, Math.Sqrt(3.0) + 0.15,
                Math.Sqrt(3.0) + 0.24, Math.Sqrt(3.0) + 0.22, -Math.Sqrt(3.0) - 0.25,
                /* coordinates of vertices of tetrahedron*/
                x, y, z,
                /* coordinates of point we want colour of */
                depth));
            /* subdivision depth */
        }

        double planet(double a, double b, double c, double d, double ass, double bs, double cs, double ds,
            double ax, double ay, double az, double bx, double by, double bz, double cx, double cy, double cz,
            double dx, double dy, double dz,
            double x, double y, double z, int level)
        {
            double abx, aby, abz, acx, acy, acz, adx, ady, adz;
            double bcx, bcy, bcz, bdx, bdy, bdz, cdx, cdy, cdz;
            double lab, lac, lad, lbc, lbd, lcd;
            double ex, ey, ez, e, es, es1, es2, es3;
            double eax, eay, eaz, epx, epy, epz;
            double ecx, ecy, ecz, edx, edy, edz;
            double x1, y1, z1, x2, y2, z2, l1, tmp;

            if (level > 0)
            {
                if (level == 11)
                {
                    ssa = a;
                    ssb = b;
                    ssc = c;
                    ssd = d;
                    ssas = ass;
                    ssbs = bs;
                    sscs = cs;
                    ssds = ds;
                    ssax = ax;
                    ssay = ay;
                    ssaz = az;
                    ssbx = bx;
                    ssby = by;
                    ssbz = bz;
                    sscx = cx;
                    sscy = cy;
                    sscz = cz;
                    ssdx = dx;
                    ssdy = dy;
                    ssdz = dz;
                }

                abx = ax - bx;
                aby = ay - by;
                abz = az - bz;
                acx = ax - cx;
                acy = ay - cy;
                acz = az - cz;
                lab = abx * abx + aby * aby + abz * abz;
                lac = acx * acx + acy * acy + acz * acz;

                /* reorder vertices so ab is longest edge */
                if (lab < lac)
                    return (planet(a, c, b, d, ass, cs, bs, ds,
                        ax, ay, az, cx, cy, cz, bx, by, bz, dx, dy, dz,
                        x, y, z, level));
                adx = ax - dx;
                ady = ay - dy;
                adz = az - dz;
                lad = adx * adx + ady * ady + adz * adz;
                if (lab < lad)
                    return (planet(a, d, b, c, ass, ds, bs, cs,
                        ax, ay, az, dx, dy, dz, bx, @by, bz, cx, cy, cz,
                        x, y, z, level));
                bcx = bx - cx;
                bcy = @by - cy;
                bcz = bz - cz;
                lbc = bcx * bcx + bcy * bcy + bcz * bcz;
                if (lab < lbc)
                    return (planet(b, c, a, d, bs, cs, ass, ds,
                        bx, @by, bz, cx, cy, cz, ax, ay, az, dx, dy, dz,
                        x, y, z, level));
                bdx = bx - dx;
                bdy = @by - dy;
                bdz = bz - dz;
                lbd = bdx * bdx + bdy * bdy + bdz * bdz;

                if (lab < lbd)
                    return (planet(b, d, a, c, bs, ds, ass, cs,
                        bx, @by, bz, dx, dy, dz, ax, ay, az, cx, cy, cz,
                        x, y, z, level));
                cdx = cx - dx;
                cdy = cy - dy;
                cdz = cz - dz;
                lcd = cdx * cdx + cdy * cdy + cdz * cdz;
                if (lab < lcd)
                    return (planet(c, d, a, b, cs, ds, ass, bs,
                        cx, cy, cz, dx, dy, dz, ax, ay, az, bx, @by, bz,
                        x, y, z, level));
                /* ab is longest, so cut ab */
                es = rand2(ass, bs);
                es1 = rand2(es, es);
                es2 = 0.5 + 0.1 * rand2(es1, es1);
                es3 = 1.0 - es2;
                if (ax < bx)
                {
                    ex = es2 * ax + es3 * bx;
                    ey = es2 * ay + es3 * @by;
                    ez = es2 * az + es3 * bz;
                }
                else if (ax > bx)
                {
                    ex = es3 * ax + es2 * bx;
                    ey = es3 * ay + es2 * @by;
                    ez = es3 * az + es2 * bz;
                }
                else
                {
                    /* ax==bx, very unlikely to ever happen */
                    ex = 0.5 * ax + 0.5 * bx;
                    ey = 0.5 * ay + 0.5 * @by;
                    ez = 0.5 * az + 0.5 * bz;
                }

                if (lab > 1.0) lab = Math.Pow(lab, 0.5);
                /* decrease contribution for very long distances */

                /* new altitude is: */
                e = 0.5 * (a + b) /* average of end points */
                    + es * DD1 * Math.Abs(a - b) /* plus contribution for altitude diff */
                    + es1 * DD2 * Math.Pow(lab, POW); /* plus contribution for distance */
                eax = ax - ex;
                eay = ay - ey;
                eaz = az - ez;
                epx = x - ex;
                epy = y - ey;
                epz = z - ez;
                ecx = cx - ex;
                ecy = cy - ey;
                ecz = cz - ez;
                edx = dx - ex;
                edy = dy - ey;
                edz = dz - ez;
                if ((eax * ecy * edz + eay * ecz * edx + eaz * ecx * edy
                     - eaz * ecy * edx - eay * ecx * edz - eax * ecz * edy) *
                    (epx * ecy * edz + epy * ecz * edx + epz * ecx * edy
                     - epz * ecy * edx - epy * ecx * edz - epx * ecz * edy) > 0.0)
                    return (planet(c, d, a, e, cs, ds, ass, es,
                        cx, cy, cz, dx, dy, dz, ax, ay, az, ex, ey, ez,
                        x, y, z, level - 1));

                return (planet(c, d, b, e, cs, ds, bs, es,
                    cx, cy, cz, dx, dy, dz, bx, @by, bz, ex, ey, ez,
                    x, y, z, level - 1));
            }

            return ((a + b + c + d) / 4);
        }
    }
}