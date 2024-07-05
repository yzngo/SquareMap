using System;
using TMPro;
using UnityEngine;

namespace JoyNow.SLG
{
    /// <summary>
    /// 管理网格
    /// </summary>
    public class SquareGrid : MonoBehaviour
    {
        public const int width = 15;
        public const int height = 15;

        public Color defaultColor = Color.white;
        public Color touchedColor = Color.cyan;
        
        public SquareCell cellPrefab;
        public TextMeshProUGUI mapCellLabelPrefab;
        private Canvas gridCanvas;
        private SquareMesh squareMesh;

        private SquareCell[] cells;

        private SquareCell selectCell;

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

        /// <summary>
        /// x,z 为网格坐标，i 为索引
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <param name="i"></param>
        private void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = x * MapMetrics.CellEdgeLength;
            position.y = 0f;
            position.z = z * MapMetrics.CellEdgeLength;

            SquareCell cell = cells[i] = Instantiate(cellPrefab);
            cell.Index = i;
            cell.Coordinates = new CellCoordinates(x, z);
            cell.color = defaultColor;
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
            cell.name = "Cell-" + cell.Index.ToString("0000") + "  (" + x + "," + z + ")";
            
            TextMeshProUGUI label = Instantiate(mapCellLabelPrefab);
            label.rectTransform.SetParent(gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = cell.Coordinates.ToString();
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit))
            {
                TouchCell(hit.point);
            }
        }

        private void TouchCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            // CellCoordinates coordinates = CellCoordinates.FromPosition(position);
            int index = CellCoordinates.ToIndex(position);
            Debug.Log(index);
            SquareCell cell = cells[index];
            if (selectCell != null)
            {
                selectCell.color = defaultColor;
            }
            selectCell = cell;
            cell.color = touchedColor;
            squareMesh.Triangulate(cells);
        }
    }
}