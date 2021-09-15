using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GameLogic
    {
        SnakeGenerator sgen;
        FoodGenerator fgen;
        int[,] board;
        int[] head, tail;
        int row, column;
        Dictionary<Direction, Func<int[], int[]>> moveMap = new Dictionary<Direction, Func<int[], int[]>>()
        {
            {Direction.Down,point=>new int[2]{point[0]+1,point[1] } },
            {Direction.Left,point=>new int[2]{point[0],point[1]-1 } },
            {Direction.Right,point=>new int[2]{point[0],point[1]+1 } },
            {Direction.Up,point=>new int[2]{point[0]-1,point[1] } }
        };
        public GameLogic(int rowCount, int columnCount)
        {
            row = rowCount;
            column = columnCount;
            board = new int[rowCount, columnCount];
            sgen = new SnakeGenerator(rowCount, columnCount);
            fgen = new FoodGenerator(rowCount, columnCount);
            var snake = sgen.GenSnake();
            head = snake[0];
            tail = snake[2];
            foreach (int[] point in snake)
            {
                board[point[0], point[1]] = 1;
            }
            int[] food = ValidFood();
            board[food[0], food[1]] = -1;
        }
        int[] ValidFood()
        {
            int[] food;
            int times = 0;
            do
            {
                food = fgen.GenFood();
                times++;
            } while (board[food[0], food[1]] != 0 && times < 20);
            if (times == 20)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            return new int[] { i, j };
                        }
                    }
                }
            }
            return food;
        }
        public bool Move(Direction direction, out IReadOnlyCollection<int[]> changedPoints)
        {
            var res = new List<int[]>();
            changedPoints = res.AsReadOnly();
            int[] newHead = moveMap[direction](head);
            if (OutOfBound(newHead) || InSnake(newHead)) return false;
            if (InEmpty(newHead))
            {
                res.Add(new int[3] { tail[0], tail[1], 0 });
                int[] oldTail = tail;
                tail = moveMap[(Direction)board[tail[0], tail[1]]](tail);
                board[oldTail[0], oldTail[1]] = 0;
            }
            else
            {
                int[] newFood = ValidFood();
                if (newFood != null)
                {
                    res.Add(new int[3] { newFood[0], newFood[1], -1 });
                    board[newFood[0], newFood[1]] = -1;
                }
            }
            board[head[0], head[1]] = (int)direction;
            head = newHead;
            res.Add(new int[3] { head[0], head[1], 1 });
            board[head[0], head[1]] = 1;
            return true;
        }
        public int[,] GetMap() => (int[,])board.Clone();
        bool OutOfBound(int[] point) => point[0] < 0 || point[0] >= row || point[1] < 0 || point[1] >= column;
        bool InSnake(int[] point) => board[point[0], point[1]] > 0;
        bool InEmpty(int[] point) => board[point[0], point[1]] == 0;
    }
}
