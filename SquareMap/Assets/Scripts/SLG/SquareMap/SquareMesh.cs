using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace JoyNow.SLG
{
    /// <summary>
    /// 地图模型
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class SquareMesh : MonoBehaviour
    {
        private Mesh squareMesh;
        private List<Vector3> vertices;
        private List<Color> colors;
        private List<int> triangles;
        private List<Vector2> uvs;
        

        private void Awake()
        {
            GetComponent<MeshFilter>().mesh = squareMesh = new Mesh();
            squareMesh.name = "Square Mesh";
        }
        
        public void Triangulate(SquareCell[] cells)
        {
            squareMesh.Clear();
            vertices = ListPool<Vector3>.Get();
            colors = ListPool<Color>.Get();
            triangles = ListPool<int>.Get();
            uvs = ListPool<Vector2>.Get();
            vertices.Clear();
            colors.Clear();
            triangles.Clear();
            for (int i = 0; i < cells.Length; i++)
            {
                Triangulate(cells[i]);
            }
            squareMesh.SetVertices(vertices);
            squareMesh.SetColors(colors);
            squareMesh.SetTriangles(triangles, 0);
            ListPool<Vector3>.Release(vertices);
            ListPool<Color>.Release(colors);
            ListPool<int>.Release(triangles);
            ListPool<Vector2>.Release(uvs);
            squareMesh.RecalculateNormals();
        }
       
        private void Triangulate(SquareCell cell)
        {
            Vector3 center = cell.Position;
            
            int vertexIndex = vertices.Count;
            // 添加顶点和颜色
            for(var d = SquareDirection.North; d <= SquareDirection.West; d++)
            {
                vertices.Add(center + MapMetrics.GetFirstCorner(d));
                colors.Add(cell.TerrainType.GetColorTip());
            }
            // 添加 UV
            AddUV(new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1));
            AddUV(new Vector2(1, 0), new Vector2(0, 1), new Vector2(0, 0));
            // 添加三角形索引
            AddTriangle(vertexIndex, vertexIndex + 1, vertexIndex + 2);
            AddTriangle(vertexIndex, vertexIndex + 2, vertexIndex + 3);
        }

        private void AddUV(Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
            uvs.Add(uv1);
            uvs.Add(uv2);
            uvs.Add(uv3);
        }
        
        private void AddTriangle(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            triangles.Add(vertexIndex1);
            triangles.Add(vertexIndex2);
            triangles.Add(vertexIndex3);
        }
    }
}