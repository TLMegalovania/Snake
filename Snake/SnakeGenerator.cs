using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Snake
{
    public class SnakeGenerator
    {
        Random rand;
        int x, y;
        public SnakeGenerator(int rowCount,int columnCount)
        {
            rand = new Random();
            this.x = rowCount - 1;
            this.y = columnCount - 1;
        }
        public List<int[]> GenSnake()
        {
            int[] ranPoint = new int[2] { rand.Next(1, x), rand.Next(1, y) };
            var res = new List<int[]>() { (int[])ranPoint.Clone(), (int[])ranPoint.Clone(), (int[])ranPoint.Clone() };
            res[0][0]--;
            res[2][0]++;
            return res;
        }
    }
}
