using System;
using UnityEngine;

namespace JoyNow.SLG
{
    [Serializable]
    public struct CellCoordinates
    {
        [SerializeField]
        private int x, z;
        public int X => x;

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
    }
}