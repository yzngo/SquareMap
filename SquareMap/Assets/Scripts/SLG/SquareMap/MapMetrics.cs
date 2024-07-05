using UnityEngine;

namespace JoyNow.SLG
{
    /// <summary>
    /// yzntodo 改成 ScriptableObject 单例
    /// </summary>
    public static class MapMetrics
    {
        public static float CellEdgeLength = 5;

        public static float HalfCellEdgeLength = 0.5f * CellEdgeLength;

        private static Vector3[] cellCorners =
        {
            new Vector3(-HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, -HalfCellEdgeLength),
            new Vector3(-HalfCellEdgeLength, 0, -HalfCellEdgeLength),
            new Vector3(-HalfCellEdgeLength, 0, HalfCellEdgeLength)
        };

        public static Vector3 GetFirstCorner(SquareDirection direction)
        {
            return cellCorners[(int)direction];
        }
        
        public static Vector3 GetSecondCorner(SquareDirection direction)
        {
            return cellCorners[(int)direction + 1];
        }

    }
}