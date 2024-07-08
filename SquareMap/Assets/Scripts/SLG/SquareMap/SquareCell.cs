using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class SquareCell : MonoBehaviour
    {
        // yzntodo inspector 设置成不可编辑 
        public int Index;
        [JsonIgnore]
        public CellCoordinates Coordinates => CellCoordinates.FromIndex(Index);
        [JsonIgnore]
        public Vector3 Position => transform.localPosition;
        
        private CellTerrainType terrainType = CellTerrainType.Plain;

        public CellTerrainType TerrainType
        {
            get => terrainType;
            set
            {
                if (terrainType != value)
                {
                    terrainType = value;
                    uiLabel.text = Coordinates + "\n" + TerrainType;
                    RefreshChunk();
                }
            }
        }
        
        // 摆放在地图上的建筑等，增加地图趣味性，没有实际用处
        private int cellFeatureId = -1;  // CellFeatureConfig

        public int CellFeatureId
        {
            get => cellFeatureId;
            set {
                if (cellFeatureId != value)
                {
                    cellFeatureId = value;
                    RefreshChunk();
                }
            }
        }
        

        public CellStates CellStates;
        
        [JsonIgnore]
        public TextMeshProUGUI uiLabel;

        [JsonIgnore]
        public SquareGridChunk chunk;

        [SerializeField]
        private bool[] EdgePassable = new bool[] {true, true, true, true};
        
        public bool IsPassableEdge(CellDirection direction)
        {
            return EdgePassable[(int) direction];
        }

        public void InitCell(int index)
        {
            Index = index;
            TerrainType = CellTerrainType.Plain;
            CellStates.AddState(CellStates.Interactable);
            uiLabel.text = Coordinates + "\n" + TerrainType;
            RefreshChunk();
        }
        
        
        public SquareCell GetNeighbor(CellDirection cellDirection)
        {
            int x =  Coordinates.X;
            int z =  Coordinates.Z;
            switch (cellDirection)
            {
                case CellDirection.North:
                    z += 1;
                    break;
                case CellDirection.East:
                    x += 1;
                    break;
                case CellDirection.South:
                    z -= 1;
                    break;
                case CellDirection.West:
                    x -= 1;
                    break;
            }
            if (x < 0 || x >= SquareGrid.cellCountX || z < 0 || z >= SquareGrid.cellCountZ)
            {
                return null;
            }
            return SquareGrid.Instance.Cells[CellCoordinates.ToIndex(x, z)];       
        }
        


        private void RefreshChunk()
        {
            chunk?.Refresh();
        }

    }
}