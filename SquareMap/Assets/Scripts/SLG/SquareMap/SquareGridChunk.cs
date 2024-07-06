using System;
using UnityEngine;

namespace JoyNow.SLG
{
    public class SquareGridChunk : MonoBehaviour
    {
        private SquareCell[] cells;
        private SquareMesh squareMesh;
        private Canvas gridCanvas;

        private void Awake()
        {
            gridCanvas = GetComponentInChildren<Canvas>();
            squareMesh = GetComponentInChildren<SquareMesh>();
            cells = new SquareCell[MapMetrics.ChunkSizeX * MapMetrics.ChunkSizeZ];
        }

        public void AddCell(int index, SquareCell cell)
        {
            cells[index] = cell;
            cell.chunk = this;
            cell.transform.SetParent(transform, false);
            cell.uiRect.SetParent(gridCanvas.transform, false);
        }

        public void Refresh()
        {
            enabled = true;
        }

        private void LateUpdate()
        {
            squareMesh.Triangulate(cells);
            enabled = false;
        }
        
        public void ShowUI(bool visible)
        {
            gridCanvas.gameObject.SetActive(visible);
        }
    }
}