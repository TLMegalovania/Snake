using System.Collections.Generic;

namespace Snake
{
    public class GameObj
    {
        protected List<int[]> points;
        protected int[] head;
        public IReadOnlyList<int> Head => head;
        public IReadOnlyList<IReadOnlyList<int>> Points => points;
        protected GameObj(List<int[]> points)
        {
            this.points = points;
            head = points[0];
        }
    }
}
