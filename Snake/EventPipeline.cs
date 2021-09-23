using System.Windows;
using System.Windows.Controls;

namespace Snake
{
    class EventPipeline
    {
        readonly SnakeInput input;
        readonly GameLogic logic;
        readonly Render render;
        public EventPipeline(UIElement inputSource, Grid grid)
        {
            input = new SnakeInput(inputSource);
            logic = new GameLogic(grid.RowDefinitions.Count, grid.ColumnDefinitions.Count);
            render = new Render(grid, logic.GetMap());
        }
        public bool StartPipeline(out int score)
        {
            var dir = input.Next;
            if (!logic.Move(dir, out var points, out score)) return false;
            render.Paint(points);
            return true;
        }
    }
}
