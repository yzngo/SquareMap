using System;
using TMPro;
using UnityEngine;

namespace JoyNow.SLG
{
    public class SquareGrid : MonoBehaviour
    {
        public int width = 15;
        public int height = 15;
        
        public SquareCell cellPrefab;
        public TextMeshProUGUI mapCellLabelPrefab;
        private Canvas gridCanvas;
        private SquareMesh squareMesh;

        private SquareCell[] cells;

        private void Awake()
        {
            gridCanvas = GetComponentInChildren<Canvas>();
            squareMesh = GetComponentInChildren<SquareMesh>();
            cells = new SquareCell[width * height];
            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }

        private void Start()
        {
            // 在 squareMesh Awake 之后，三角化网格
            squareMesh.Triangulate(cells);
        }

        private void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = x * MapMetrics.CellEdgeLength;
            position.y = 0f;
            position.z = z * MapMetrics.CellEdgeLength;

            SquareCell cell = cells[i] = Instantiate(cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            
            TextMeshProUGUI label = Instantiate(mapCellLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = "(" + x + "," + z + ")";
        }
        
    }
}