using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeObj : GameObj
    {
        public IList<int> Tail { get; set; }

        readonly Dictionary<Direction, Func<IList<int>, int[]>> moveMap = new()
        {
            {Direction.Down,point=>new int[3]{point[0]+1,point[1],0 } },
            {Direction.Left,point=>new int[3]{point[0],point[1]-1,0 } },
            {Direction.Right,point=>new int[3]{point[0],point[1]+1,0 } },
            {Direction.Up,point=>new int[3]{point[0]-1,point[1],0 } }
        };

        public SnakeObj(List<int[]> points) : base(points)
        {
            Tail = points[^1];
        }
        public void MoveHead(Direction direction)
        {
            head = moveMap[direction](head);
        }
        public void MoveTail()
        {
            Tail = moveMap[(Direction)Tail[2]](Tail);
        }

    }
}
