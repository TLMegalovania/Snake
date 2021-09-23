using System;
using System.Collections.Generic;

namespace Snake
{
    public class SnakeGenerator
    {
        readonly Random rand;
        readonly int x, y;
        public SnakeGenerator(int rowCount, int columnCount)
        {
            rand = new Random();
            x = rowCount - 1;
            y = columnCount - 1;
        }
        public SnakeObj GenSnake()
        {
            int[] ranPoint = new int[3] { rand.Next(1, x), rand.Next(1, y), 1 };
            var res = new List<int[]>() { (int[])ranPoint.Clone(), (int[])ranPoint.Clone(), (int[])ranPoint.Clone() };
            res[0][0]--;
            res[2][0]++;
            return new(res);
        }
    }
}
