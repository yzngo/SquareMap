using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class SquareCell : MonoBehaviour
    {
        // yzntodo 设置成不可编辑 
        public int Index;
        
        public CellCoordinates Coordinates;

        public Color color;
    }
}