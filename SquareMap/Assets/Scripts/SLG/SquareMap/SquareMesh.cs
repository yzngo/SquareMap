using System;
using System.Collections.Generic;
using UnityEngine;

namespace JoyNow.SLG
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class SquareMesh : MonoBehaviour
    {
        private Mesh squareMesh;
        private List<Vector3> vertices;
        private List<int> triangles;

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = squareMesh = new Mesh();
            squareMesh.name = "Square Mesh";
            vertices = new List<Vector3>();
            triangles = new List<int>();
        }
        
        public void Triangulate(SquareCell[] cells)
        {
            squareMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            for (int i = 0; i < cells.Length; i++)
            {
                Triangulate(cells[i]);
            }
            squareMesh.vertices = vertices.ToArray();
            squareMesh.triangles = triangles.ToArray();
            squareMesh.RecalculateNormals();
        }
        
        private void Triangulate(SquareCell cell)
        {
            Vector3 center = cell.transform.localPosition;
            for (int i = 0; i < 4; i++)
            {
                AddTriangle(center, center + MapMetrics.CellCorners[i], center + MapMetrics.CellCorners[(i + 1) % 4]);
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
    }
}