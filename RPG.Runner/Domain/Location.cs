using System;
using System.Collections.Generic;
using System.Drawing;
using RPG.Core;
using RPG.Math;

namespace RPG.Runner.Domain
{
    public class Location : IAspect
    {
        public SizeF Bounds { get; } = new SizeF(4000, 2000);

        private static readonly Random Rand = new Random();

        public Character Character { get; } = new Character();
        public List<Tree> Trees { get; } = new List<Tree>();
        public List<Bush> Bushes { get; } = new List<Bush>();
        public List<Stone> Stones { get; } = new List<Stone>();
        public List<Building> Buildings { get; } = new List<Building>();

        public Location()
        {
            for (var i = 0; i < 25; ++i)
            {
                var x = -Bounds.Width / 2 + (float)Rand.NextDouble() * Bounds.Width;
                var y = -Bounds.Height / 2 + (float)Rand.NextDouble() * Bounds.Height;
                Trees.Add(new Tree(Matrix.CreateTranslate(x, y)));
            }
            for (var i = 0; i < 20; ++i)
            {
                var x = -Bounds.Width / 2 + (float)Rand.NextDouble() * Bounds.Width;
                var y = -Bounds.Height / 2 + (float)Rand.NextDouble() * Bounds.Height;
                Bushes.Add(new Bush(Matrix.CreateTranslate(x, y)));
            }
            for (var i = 0; i < 15; ++i)
            {
                var x = -Bounds.Width / 2 + (float)Rand.NextDouble() * Bounds.Width;
                var y = -Bounds.Height / 2 + (float)Rand.NextDouble() * Bounds.Height;
                Stones.Add(new Stone(Matrix.CreateTranslate(x, y)));
            }
            for (var i = 0; i < 2; ++i)
            {
                var x = -Bounds.Width / 2 + (float)Rand.NextDouble() * Bounds.Width;
                var y = -Bounds.Height / 2 + (float)Rand.NextDouble() * Bounds.Height;
                Buildings.Add(new Building(Matrix.CreateTranslate(x, y)));
            }
        }
    }
} 