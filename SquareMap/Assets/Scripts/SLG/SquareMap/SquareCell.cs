using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class SquareCell : MonoBehaviour
    {
        // yzntodo inspector 设置成不可编辑 
        public int Index;
        public CellCoordinates Coordinates => CellCoordinates.FromIndex(Index);
        public Vector3 Position => transform.localPosition;
        

        public Color color = Color.white;

        public RectTransform uiRect;

        public SquareGridChunk chunk;

        [SerializeField]
        private bool[] EdgePassable = new bool[] {true, true, true, true};
        
        public bool IsEdgePassable(SquareDirection direction)
        {
            return EdgePassable[(int) direction];
        }

        public void SetColor(Color color)
        {
            if (this.color == color) return;
            this.color = color;
            RefreshChunk();
        }

        private void RefreshChunk()
        {
            chunk?.Refresh();
        }

    }
}