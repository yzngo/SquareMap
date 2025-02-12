﻿using System;
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
            if (x < 0 || x >= SquareGrid.cellCountX)
            {
                throw new ArgumentOutOfRangeException(nameof(x) + " " + x, "Coordinates out of range");
            }
            if (z < 0 || z >= SquareGrid.cellCountZ)
            {
                throw new ArgumentOutOfRangeException(nameof(z) + " " + z, "Coordinates out of range");
            }
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

        public static bool operator ==(CellCoordinates a, CellCoordinates b)
        {
            return a.X == b.X && a.Z == b.Z;
        }
        
        public static bool operator !=(CellCoordinates a, CellCoordinates b)
        {
            return a.X != b.X || a.Z != b.Z;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is CellCoordinates other)
            {
                return X == other.X && Z == other.Z;
            }
            return false;
        }
        

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + X;
            hash = hash * 23 + Z;
            return hash;
        }
    }
}