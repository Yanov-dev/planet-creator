using planet_craator.engine.Primitives;

namespace planet_craator.engine.GenerationAlgorithm
{
    public abstract class GenerationAlgorithmBase
    {
        public double Seed { get; }

        public GenerationAlgorithmBase(double seed)
        {
            Seed = seed;
        }

        public abstract double GetAlt(Point3D location, int depth);
        
        public abstract (double min, double max) GetRange();
    }
}