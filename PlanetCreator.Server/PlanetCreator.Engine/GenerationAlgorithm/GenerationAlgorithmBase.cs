﻿using PlanetCreator.Engine.Primitives;

namespace PlanetCreator.Engine.GenerationAlgorithm
{
    public abstract class GenerationAlgorithmBase
    {
        public double Seed { get; }

        public GenerationAlgorithmBase(double seed)
        {
            Seed = seed;
        }

        public abstract double GetAlt(Point3D location, int depth);
        
        public abstract Range GetRange();
    }
}