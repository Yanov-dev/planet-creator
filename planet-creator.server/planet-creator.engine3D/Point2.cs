namespace planet_creator.engine3D
{
    public struct Point2
    {
        public int X;
        public int Y;

        public Point2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point2 MiddlePoint(Point2 p1, Point2 p2)
        {
            return new Point2((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        public override string ToString()
        {
            return $"{X} : {Y}";
        }

        public override int GetHashCode()
        {
            return X << 16 + Y;
        }
    }
}