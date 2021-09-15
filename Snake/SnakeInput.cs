using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Snake
{
    class SnakeInput
    {
        public Direction Next 
        {
            get
            {
                last = next;
                return next;
            }
        }
        Direction last,next;
        Dictionary<Key, Direction> diction = new Dictionary<Key, Direction>()
        {
            {Key.W,Direction.Up },
            {Key.A,Direction.Left },
            {Key.S,Direction.Down },
            {Key.D,Direction.Right }
        };
        Dictionary<Direction, Direction> opposite = new Dictionary<Direction, Direction>()
        {
            {Direction.Down,Direction.Up },
            {Direction.Up,Direction.Down },
            {Direction.Left,Direction.Right },
            {Direction.Right,Direction.Left }
        };
        public SnakeInput(UIElement source)
        {
            source.KeyDown += InputFilter;
            next = Direction.Up;
            last = Next;
        }
        void InputFilter(object sender,KeyEventArgs args)
        {
            Key pressed = args.Key;
            if (!diction.ContainsKey(pressed)) return;
            var mayNext = diction[pressed];
            if (opposite[mayNext] != last) next = mayNext;
        }
    }
}
