using UnityEngine;

namespace JoyNow.SLG
{
    public class MapGrid : MonoBehaviour
    {
        public int width = 6;
        public int height = 6;
        
        public MapCell cellPrefab;


        private MapCell[] cells;

        private void Awake()
        {
            cells = new MapCell[width * height];
            for (int z = 0, i = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    CreateCell(x, z, i++);
                }
            }
        }
        
        private void CreateCell(int x, int z, int i)
        {
            Vector3 position;
            position.x = x * MapMetrics.CellEdgeLength;
            position.y = 0f;
            position.z = z * MapMetrics.CellEdgeLength;

            MapCell cell = cells[i] = Instantiate(cellPrefab);
            cell.transform.SetParent(transform, false);
            cell.transform.localPosition = position;
        }
    }
}