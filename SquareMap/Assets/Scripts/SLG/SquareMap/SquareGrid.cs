using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    /// <summary>
    /// 管理四边形网格
    /// </summary>
    public class SquareGrid : MonoBehaviour
    {
        public static SquareGrid Instance;
        // x 方向 Chunk 数量
        public int ChunkCountX = 10;
        // z 方向 Chunk 数量
        public int ChunkCountZ = 10;
        // x 方向格子数量
        public static int cellCountX;
        // z 方向格子数量
        public static int cellCountZ;
        // 格子宽度
        public float gridWidth;
        // 格子高度
        public float gridHeight;
        

        public SquareCell cellPrefab;
        public TextMeshProUGUI mapCellLabelPrefab;
        public SquareGridChunk chunkPrefab;
        
        public SquareGridChunk[] Chunks { get; private set; }
        public SquareCell[] Cells { get; private set; }
        
        private BoxCollider boxCollider;

        public SquareCell SelectedCell;

        private void Awake()
        {
            Instance = this;
            cellCountX = ChunkCountX * MapMetrics.ChunkSizeX;
            cellCountZ = ChunkCountZ * MapMetrics.ChunkSizeZ;
            
            gridWidth = cellCountX * MapMetrics.CellEdgeLength; 
            gridHeight = cellCountZ * MapMetrics.CellEdgeLength;
            
            CreateChunks();
            CreateCells();
            
            // 设置碰撞体的大小和位置
            
            boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(gridWidth, 0, gridHeight);
            boxCollider.center = new Vector3(gridWidth * 0.5f - MapMetrics.HalfCellEdgeLength, 0, gridHeight * 0.5f - MapMetrics.HalfCellEdgeLength);
        }

        private void CreateChunks()
        {
            Chunks = new SquareGridChunk[ChunkCountX * ChunkCountZ];
            
            for (int z = 0, i = 0; z < ChunkCountZ; z++) {
                for (int x = 0; x < ChunkCountX; x++) {
                    var chunk = Chunks[i++] = Instantiate(chunkPrefab);
                    chunk.transform.SetParent(transform);
                }
            }
        }

        private void CreateCells()
        {
            Cells = new SquareCell[cellCountX * cellCountZ];
            for (int z = 0, i = 0; z < cellCountZ; z++)
            {
                for (int x = 0; x < cellCountX; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
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

            SquareCell cell = Cells[i] = Instantiate(cellPrefab);
            cell.Index = i;
            cell.transform.localPosition = position;
            cell.name = "Cell-" + cell.Index.ToString("0000") + "  (" + x + "," + z + ")";
            
            TextMeshProUGUI label = Instantiate(mapCellLabelPrefab);
            cell.uiLabel = label;
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            cell.SetTerrainType(CellTerrainType.Land);
            
            AddCellToChunk(x, z, cell);
        }
        
        private void AddCellToChunk(int x, int z, SquareCell cell)
        {
            int chunkX = x / MapMetrics.ChunkSizeX;
            int chunkZ = z / MapMetrics.ChunkSizeZ;
            SquareGridChunk chunk = Chunks[chunkX + chunkZ * ChunkCountX];
            int localX = x - chunkX * MapMetrics.ChunkSizeX;
            int localZ = z - chunkZ * MapMetrics.ChunkSizeZ;
            chunk.AddCell(localX + localZ * MapMetrics.ChunkSizeX, cell);
        }
        
        public SquareCell GetCell(CellCoordinates coordinates)
        {
            return GetCell(CellCoordinates.ToIndex(coordinates));
        }

        public SquareCell GetCell(int index)
        {
            if (index < 0 || index >= Cells.Length)
            {
                return null;
            }
            return Cells[index];
        }

        public SquareCell GetCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            int index = CellCoordinates.ToIndex(position);
            return GetCell(index);
        }



        public void ShowUI(bool visible)
        {
            for (int i = 0; i < Chunks.Length; i++)
            {
                Chunks[i].ShowUI(visible);
            }
        }
    }
}