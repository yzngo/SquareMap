using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JoyNow.SLG
{
    public class SquareMapEditor : MonoBehaviour
    {
        public Color[] colors;
        public SquareGrid squareGrid;
        private Color activeColor;

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
                var squareCell = squareGrid.GetCell(hit.point);
                EditorCell(squareCell);
            }
        }

        public void EditorCell(SquareCell cell)
        {
            cell.color = activeColor;
            squareGrid.Refresh();
        }

        public void SelectColor(int index)
        {
            activeColor = colors[index];
        }
    }
}