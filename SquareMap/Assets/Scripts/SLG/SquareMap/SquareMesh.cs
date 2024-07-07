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
            
            for (int i = 0; i < cells.Length; i++)
            {
                Triangulate(cells[i]);
            }
            squareMesh.SetVertices(vertices);
            squareMesh.SetColors(colors);
            squareMesh.SetTriangles(triangles, 0);
            squareMesh.SetUVs(0, uvs);
            
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
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(1, 1));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(0, 0));
            
            // 添加三角形索引
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
            
            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);
        }

    }
}