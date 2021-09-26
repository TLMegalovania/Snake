using System.Collections.Generic;

namespace Snake
{
    class GameLogic
    {
        readonly SnakeGenerator sgen;
        readonly FoodGenerator fgen;
        readonly SnakeObj snakeObj;
        readonly List<FoodObj> foodObjs;
        readonly int[,] board;
        readonly int row, column;
        private int score;
        private readonly int foodCount,wallCount;

        public GameLogic(int rowCount, int columnCount)
        {
            foodCount = 50;
            wallCount = 50;
            row = rowCount;
            column = columnCount;
            board = new int[rowCount, columnCount];
            sgen = new SnakeGenerator(rowCount, columnCount);
            fgen = new FoodGenerator(rowCount, columnCount);
            snakeObj = sgen.GenSnake();
            foreach (int[] point in snakeObj.Points)
            {
                board[point[0], point[1]] = 1;
            }
            foodObjs = new(foodCount);
            for (int i = 0; i < foodCount; i++)
            {
                var foodObj = ValidFood();
                foodObjs.Add(foodObj);
                board[foodObj.Head[0], foodObj.Head[1]] = -1;
            }
            for (int i = 0; i < wallCount; i++)
            {
                var wallObj = ValidFood();
                board[wallObj.Head[0], wallObj.Head[1]] = -2;
            }
        }
        FoodObj ValidFood()
        {
            FoodObj food;
            int times = 0;
            do
            {
                food = fgen.GenFood();
                times++;
            } while (board[food.Head[0], food.Head[1]] != 0 && times < 20);
            if (times == 20)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        if (board[i, j] == 0)
                        {
                            return new(new() { new int[] { i, j } });
                        }
                    }
                }
            }
            return food;
        }
        public bool Move(Direction direction, out IReadOnlyCollection<int[]> changedPoints, out int score)
        {
            score = this.score;
            var res = new List<int[]>();
            changedPoints = res.AsReadOnly();
            var oldHead = new int[2] { snakeObj.Head[0], snakeObj.Head[1] };
            snakeObj.MoveHead(direction);
            var head = snakeObj.Head;
            var tail = snakeObj.Tail;
            if (OutOfBound(head) || InSnake(head) || InWall(head))
            {
                return false;
            }
            if (InEmpty(head))
            {
                res.Add(new int[3] { tail[0], tail[1], 0 });
                snakeObj.MoveTail();
                board[tail[0], tail[1]] = 0;
                tail = snakeObj.Tail;
                snakeObj.Tail[2] = board[tail[0], tail[1]];
            }
            else if(InFood(head))
            {
                score = ++this.score;
                var nfood = ValidFood();
                if (nfood != null)
                {
                    var newFood = nfood.Head;
                    res.Add(new int[3] { newFood[0], newFood[1], -1 });
                    board[newFood[0], newFood[1]] = -1;
                }
            }
            board[oldHead[0], oldHead[1]] = (int)direction;
            res.Add(new int[3] { head[0], head[1], 1 });
            board[head[0], head[1]] = 1;
            return true;
        }
        public int[,] GetMap() => (int[,])board.Clone();
        bool OutOfBound(IReadOnlyList<int> point) => point[0] < 0 || point[0] >= row || point[1] < 0 || point[1] >= column;
        bool InSnake(IReadOnlyList<int> point) => board[point[0], point[1]] > 0;
        bool InEmpty(IReadOnlyList<int> point) => board[point[0], point[1]] == 0;
        bool InFood(IReadOnlyList<int> point) => board[point[0], point[1]] == -1;
        bool InWall(IReadOnlyList<int> point) => board[point[0], point[1]] == -2;
    }
}
