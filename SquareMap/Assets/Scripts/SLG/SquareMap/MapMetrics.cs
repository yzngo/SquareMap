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

        public static Vector3[] CellCorners =
        {
            new Vector3(-HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, HalfCellEdgeLength),
            new Vector3(HalfCellEdgeLength, 0, -HalfCellEdgeLength),
            new Vector3(-HalfCellEdgeLength, 0, -HalfCellEdgeLength)
        };

    }
}