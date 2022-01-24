namespace Board
{
    public enum NodeDirection
    {
        N, NE, E, SE, S, SW, W, NW
    }

    public static class SquareDirectionExtension
    {
        public static NodeDirection Opposite(this NodeDirection direction) => 
            (int)direction < 4 ? (direction + 4) : (direction - 4);
        
        public static NodeDirection Previous(this NodeDirection direction) =>
            direction == NodeDirection.N ? NodeDirection.NW : (direction - 1);
        
        public static NodeDirection PreviousTwoStep(this NodeDirection direction)
        {
            if((int)direction - 2 < 0)
            {
                if ((int) direction - 2 == -1) return NodeDirection.NW;
                return NodeDirection.W;
            }
            return (direction - 2);
        }
        
        public static NodeDirection Next(this NodeDirection direction) =>
            direction == NodeDirection.NW ? NodeDirection.N : (direction + 1);
        
        public static NodeDirection NextTwoStep(this NodeDirection direction)
        {
            if((int)direction + 2 > 7)
            {
                if ((int) direction + 2 - 7 == 1) return NodeDirection.N;
                return NodeDirection.NW;
            }
            return (direction + 2);
        }
    }
}