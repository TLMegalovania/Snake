using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class FoodGenerator
    {
        Random rand;
        int x, y;
        public FoodGenerator(int rowCount, int columnCount)
        {
            rand = new Random();
            this.x = rowCount;
            this.y = columnCount;
        }
        public int[] GenFood() => new int[2] { rand.Next(x), rand.Next(y) };

    }
}
