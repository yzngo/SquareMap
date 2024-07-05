using System;
using UnityEngine;

namespace JoyNow.SLG
{
    [Serializable]
    public struct CellCoordinates
    {
        [SerializeField]
        private int x;
        public int X => x;

        [SerializeField]
        private int z;
        
        public int Z => z;
        
        public CellCoordinates(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        
        public override string ToString()
        {
            return "(" + X + "," + Z + ")";
        }

        /// <summary>
        /// 通过实际场景中位置获取网格坐标
        /// </summary>
        public static CellCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (MapMetrics.CellEdgeLength);
            float z = position.z / (MapMetrics.CellEdgeLength);
            int iX = Mathf.RoundToInt(x);
            int iZ = Mathf.RoundToInt(z);
            return new CellCoordinates(iX, iZ);
        }

        /// <summary>
        /// 从索引获取网格坐标
        /// </summary>
        public static CellCoordinates FromIndex(int index)
        {
            int x = index % SquareGrid.cellCountX;
            int z = index / SquareGrid.cellCountX;
            return new CellCoordinates(x, z);
        }

        /// <summary>
        /// 从网格坐标获取索引
        /// </summary>
        public static int ToIndex(CellCoordinates coordinates)
        {
            return coordinates.X + coordinates.Z * SquareGrid.cellCountX;
        }
        
        /// <summary>
        /// 从网格坐标获取索引
        /// </summary>
        public static int ToIndex(int x, int z)
        {
            return x + z * SquareGrid.cellCountX;
        }

        /// <summary>
        /// 通过实际场景中位置获取网格索引
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int ToIndex(Vector3 position)
        {
            return ToIndex(FromPosition(position));
        }
        
    }
}