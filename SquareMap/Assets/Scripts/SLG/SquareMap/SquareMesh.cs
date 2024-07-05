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
        private static List<Vector3> vertices = new();
        private static List<Color> colors = new();
        private static List<int> triangles = new();
        

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = squareMesh = new Mesh();
            squareMesh.name = "Square Mesh";
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
        }
       
        private void Triangulate(SquareCell cell)
        {
            Vector3 center = cell.Position;
            
            int vertexIndex = vertices.Count;
            for(var d = SquareDirection.North; d <= SquareDirection.West; d++)
            {
                vertices.Add(center + MapMetrics.GetFirstCorner(d));
                colors.Add(cell.color);
            }
            
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
            
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);
        }
    }
}