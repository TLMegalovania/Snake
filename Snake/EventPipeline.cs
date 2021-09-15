using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Snake
{
    class EventPipeline
    {
        SnakeInput input;
        GameLogic logic;
        Render render;
        public EventPipeline(UIElement inputSource, Grid grid)
        {
            input = new SnakeInput(inputSource);
            logic = new GameLogic(grid.RowDefinitions.Count, grid.ColumnDefinitions.Count);
            render = new Render(grid, logic.GetMap());
        }
        public bool StartPipeline()
        {
            var dir = input.Next;
            if (!logic.Move(dir, out var points)) return false;
            render.Paint(points);
            return true;
        }
    }
}
