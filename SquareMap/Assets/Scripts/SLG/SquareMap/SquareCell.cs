using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class SquareCell : MonoBehaviour
    {
        // yzntodo 设置成不可编辑 
        public int Index;
        
        public CellCoordinates Coordinates;

        public Color color = Color.white;

        [SerializeField]
        private bool[] EdgePassable = new bool[] {true, true, true, true};
        
        public bool IsEdgePassable(SquareDirection direction)
        {
            return EdgePassable[(int) direction];
        }
    }
}