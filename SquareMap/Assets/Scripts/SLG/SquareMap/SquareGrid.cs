using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    /// <summary>
    /// 管理网格
    /// </summary>
    public class SquareGrid : MonoBehaviour
    {
        public int ChunkCountX = 10;
        public int ChunkCountZ = 10;
        
        public static int cellCountX;
        public static int cellCountZ;

        public SquareCell cellPrefab;
        public TextMeshProUGUI mapCellLabelPrefab;
        public SquareGridChunk chunkPrefab;
        
        private SquareGridChunk[] chunks;
        private SquareCell[] cells;
        private BoxCollider boxCollider;

        public SquareCell SelectedCell;

        private void Awake()
        {
            cellCountX = ChunkCountX * MapMetrics.ChunkSizeX;
            cellCountZ = ChunkCountZ * MapMetrics.ChunkSizeZ;
            
            CreateChunks();
            CreateCells();
            
            // 设置碰撞体的大小和位置
            boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = new Vector3(MapMetrics.CellEdgeLength * SquareGrid.cellCountX, 0, MapMetrics.CellEdgeLength * SquareGrid.cellCountZ);
            boxCollider.center = new Vector3(boxCollider.size.x * 0.5f - MapMetrics.HalfCellEdgeLength, 0, boxCollider.size.z * 0.5f - MapMetrics.HalfCellEdgeLength);
        }

        private void CreateChunks()
        {
            chunks = new SquareGridChunk[ChunkCountX * ChunkCountZ];
            
            for (int z = 0, i = 0; z < ChunkCountZ; z++) {
                for (int x = 0; x < ChunkCountX; x++) {
                    var chunk = chunks[i++] = Instantiate(chunkPrefab);
                    chunk.transform.SetParent(transform);
                }
            }
        }

        private void CreateCells()
        {
            cells = new SquareCell[cellCountX * cellCountZ];
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

            SquareCell cell = cells[i] = Instantiate(cellPrefab);
            cell.Index = i;
            cell.transform.localPosition = position;
            cell.name = "Cell-" + cell.Index.ToString("0000") + "  (" + x + "," + z + ")";
            
            TextMeshProUGUI label = Instantiate(mapCellLabelPrefab);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
            label.text = cell.Coordinates.ToString();
            cell.uiRect = label.rectTransform;
            
            AddCellToChunk(x, z, cell);
        }
        
        private void AddCellToChunk(int x, int z, SquareCell cell)
        {
            int chunkX = x / MapMetrics.ChunkSizeX;
            int chunkZ = z / MapMetrics.ChunkSizeZ;
            SquareGridChunk chunk = chunks[chunkX + chunkZ * ChunkCountX];
            int localX = x - chunkX * MapMetrics.ChunkSizeX;
            int localZ = z - chunkZ * MapMetrics.ChunkSizeZ;
            chunk.AddCell(localX + localZ * MapMetrics.ChunkSizeX, cell);
        }

        public SquareCell TouchCell(Vector3 position)
        {
            position = transform.InverseTransformPoint(position);
            int index = CellCoordinates.ToIndex(position);
            SelectedCell = cells[index];
            return cells[index];
        }

        public SquareCell GetNeighbor(SquareCell squareCell, SquareDirection squareDirection)
        {
            int x =  squareCell.Coordinates.X;
            int z =  squareCell.Coordinates.Z;
            switch (squareDirection)
            {
                case SquareDirection.North:
                    z += 1;
                    break;
                case SquareDirection.East:
                    x += 1;
                    break;
                case SquareDirection.South:
                    z -= 1;
                    break;
                case SquareDirection.West:
                    x -= 1;
                    break;
            }
            if (x < 0 || x >= SquareGrid.cellCountX || z < 0 || z >= SquareGrid.cellCountZ)
            {
                return null;
            }
            return cells[CellCoordinates.ToIndex(x, z)];       
        }
    }
}