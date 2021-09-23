using System;

namespace Snake
{
    class FoodGenerator
    {
        readonly Random rand;
        readonly int x, y;
        public FoodGenerator(int rowCount, int columnCount)
        {
            rand = new Random();
            x = rowCount;
            y = columnCount;
        }
        public FoodObj GenFood() => new(new() { new int[2] { rand.Next(x), rand.Next(y) } });

    }
}
