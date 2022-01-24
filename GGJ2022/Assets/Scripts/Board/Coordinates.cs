using UnityEngine;

namespace Board
{
    [System.Serializable]
    public struct Coordinates
    {
        [SerializeField]private int _x;
        [SerializeField]private int _z;
        
        public int X => _x;
        public int Z => _z;

        public Coordinates(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public static Coordinates FromOffsetCoordinates(int x, int z)
        {
            return new Coordinates(x, z);
        }

        public override string ToString() => $"({X}, {Z})";
        public string ToStringOnSeparateLines() => $"{X}\n{Z}";
        
        public static Coordinates FromPosition(Vector3 position)
        {
            float x = position.x / BoardMetrics.Size;
            float z = position.z;

            int iX = Mathf.RoundToInt(x);
            int iZ = Mathf.RoundToInt(z);

            return new Coordinates(iX, iZ);
        }
    }
}