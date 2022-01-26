using UnityEngine;

namespace Board
{
    [System.Serializable]
    public struct Coordinates
    {
        [SerializeField]private int _x;
        [SerializeField]private int _y;
        
        public int X => _x;
        public int Y => _y;

        public Coordinates(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public static Coordinates FromOffsetCoordinates(int x, int y)
        {
            return new Coordinates(x, y);
        }

        public override string ToString() => $"({X}, {Y})";
        public string ToStringOnSeparateLines() => $"{X}\n{Y}";
        
        public static Coordinates FromPosition(Vector3 position)
        {
            float x = position.x / BoardMetrics.Size;
            float y = position.y;

            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);

            return new Coordinates(iX, iY);
        }
    }
}