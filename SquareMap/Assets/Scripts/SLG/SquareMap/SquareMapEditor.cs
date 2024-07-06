using UnityEngine;
using UnityEngine.EventSystems;

namespace JoyNow.SLG
{
    public class SquareMapEditor : MonoBehaviour
    {
        public SquareGrid squareGrid;
        
        [Range(0, 5)] 
        public int brushExtendSize = 0;
        
        public CellTerrainType activeTerrainType = CellTerrainType.Default;

        private void Awake()
        {
        }

        private void Update()
        {
            // 检测鼠标是否在 UI 物体上
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                // SquareCell lastCell = squareGrid.SelectedCell;
                // var currentCell = squareGrid.TouchCell(hit.point);
                // if (lastCell != currentCell)
                // {
                //     if (lastCell != null)
                //     {
                //         lastCell.SetColor(Color.white);
                //     }
                //     currentCell.SetColor(Color.cyan);
                //     Debug.Log(currentCell.Coordinates.ToString());
                // }
                var cell = squareGrid.GetCell(hit.point);
                EditorCells(cell);
            }
        }

        public void EditorCells(SquareCell center)
        {
            int centerX = center.Coordinates.X;
            int centerZ = center.Coordinates.Z;
            for (int z = centerZ - brushExtendSize; z <= centerZ + brushExtendSize; z++)
            {
                for (int x = centerX - brushExtendSize; x <= centerX + brushExtendSize; x++)
                {
                    var coordinates = new CellCoordinates(x, z);
                    var cell = squareGrid.GetCell(coordinates);
                    if (cell != null)
                    {
                        EditorCell(cell);
                    }
                
                }
            }
        }

        public void EditorCell(SquareCell cell)
        {
            cell.SetTerrainType(activeTerrainType);
        }

        public void SetBrushSize(int size)
        {
            brushExtendSize = size;
        }

        public void ShowUI(bool visible)
        {
            squareGrid.ShowUI(visible);
        }
    }
}