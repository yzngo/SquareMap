using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JoyNow.SLG
{
    public class SquareMapEditor : MonoBehaviour
    {
        public SquareGrid squareGrid;

        private void Awake()
        {
            SelectColor(0);
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
                SquareCell lastCell = squareGrid.SelectedCell;
                var currentCell = squareGrid.TouchCell(hit.point);
                if (lastCell != currentCell)
                {
                    if (lastCell != null)
                    {
                        lastCell.SetColor(Color.white);
                    }
                    currentCell.SetColor(Color.cyan);
                    Debug.Log(currentCell.Coordinates.ToString());
                }
            }
        }

        public void EditorCell(SquareCell cell)
        {
            // cell.SetColor(Color.cyan);
        }

        public void SelectColor(int index)
        {
            // activeColor = colors[index];
        }
    }
}