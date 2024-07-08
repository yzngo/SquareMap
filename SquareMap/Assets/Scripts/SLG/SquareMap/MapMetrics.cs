using UnityEngine;

namespace JoyNow.SLG
{
    /// <summary>
    /// yzntodo 改成 ScriptableObject 单例
    /// </summary>
    public static class MapMetrics
    {
        // 每个 Chunk x 方向格子数量 
        public static int ChunkSizeX = 10;
        // 每个 Chunk z 方向格子数量 
        public static int ChunkSizeZ = 10;
        
        // 格子边长
        public static float CellEdgeLength = 5;
        // 格子边长的一半
        public static float HalfCellEdgeLength = 0.5f * CellEdgeLength;
        // 格子对角线长度
        public static float CellDiagonalLength = Mathf.Sqrt(2) * CellEdgeLength;
        // 格子对角线长度的一半
        public static float HalfCellDiagonalLength = 0.5f * CellDiagonalLength;
        
        // 四边形格子相对于中心的顶点坐标，从左上角开始，顺时针计算
        private static Vector3[] cellCornersLocation =
        {
            new Vector3(-HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, -HalfCellEdgeLength),
            new Vector3(-HalfCellEdgeLength, 0, -HalfCellEdgeLength),
            new Vector3(-HalfCellEdgeLength, 0, HalfCellEdgeLength)
        };

        public static Vector3 GetFirstCorner(CellDirection direction)
        {
            return cellCornersLocation[(int)direction];
        }
        
        public static Vector3 GetSecondCorner(CellDirection direction)
        {
            return cellCornersLocation[(int)direction + 1];
        }

    }
}