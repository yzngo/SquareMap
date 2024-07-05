using System;
using System.Collections.Generic;
using UnityEngine;

namespace JoyNow.SLG
{
    /// <summary>
    /// 地图网格模型
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class SquareMesh : MonoBehaviour
    {
        private Mesh squareMesh;
        private List<Vector3> vertices;
        private List<Color> colors;
        private List<int> triangles;
        
        private BoxCollider boxCollider;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = squareMesh = new Mesh();
            boxCollider = gameObject.AddComponent<BoxCollider>();
            squareMesh.name = "Square Mesh";
            vertices = new List<Vector3>();
            colors = new List<Color>();
            triangles = new List<int>();
        }
        
        public void Triangulate(SquareCell[] cells)
        {
            squareMesh.Clear();
            vertices.Clear();
            colors.Clear();
            triangles.Clear();
            for (int i = 0; i < cells.Length; i++)
            {
                Triangulate(cells[i]);
            }
            squareMesh.vertices = vertices.ToArray();
            squareMesh.colors = colors.ToArray();
            squareMesh.triangles = triangles.ToArray();
            squareMesh.RecalculateNormals();

            // 设置碰撞体的大小和位置
            boxCollider.size = new Vector3(MapMetrics.CellEdgeLength * SquareGrid.width, 0, MapMetrics.CellEdgeLength * SquareGrid.height);
            boxCollider.center = new Vector3(boxCollider.size.x * 0.5f - MapMetrics.HalfCellEdgeLength, 0, boxCollider.size.z * 0.5f - MapMetrics.HalfCellEdgeLength);
        }
       
        private void Triangulate(SquareCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for (var d = SquareDirection.North; d <= SquareDirection.West; d++)
            {
                AddTriangle(center, center + MapMetrics.GetFirstCorner(d), center + MapMetrics.GetSecondCorner(d));
                AddTriangleColor(cell.color);
            }
        }
        
        private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }
        
        private void AddTriangleColor(Color color)
        {
            colors.Add(color);
            colors.Add(color);
            colors.Add(color);
        }
    }
}