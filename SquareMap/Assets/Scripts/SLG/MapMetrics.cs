using UnityEngine;

namespace JoyNow.SLG
{
    /// <summary>
    /// 改成 ScriptableObject 单例
    /// </summary>
    public static class MapMetrics
    {
        public static int CellEdgeLength = 10;

        private static int halfCellEdgeLength = (int)(0.5f * CellEdgeLength);

        public static Vector3[] CellCorners =
        {
            new Vector3(-halfCellEdgeLength, 0, halfCellEdgeLength),
            new Vector3(halfCellEdgeLength, 0, halfCellEdgeLength),
            new Vector3(halfCellEdgeLength, 0, -halfCellEdgeLength),
            new Vector3(-halfCellEdgeLength, 0, -halfCellEdgeLength)
        };

    }
}