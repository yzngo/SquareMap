using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace JoyNow.SLG
{
    public class SquareCell : MonoBehaviour
    {
        // yzntodo inspector 设置成不可编辑 
        public int Index;
        public CellCoordinates Coordinates => CellCoordinates.FromIndex(Index);
        public Vector3 Position => transform.localPosition;
        
        public CellTerrainType TerrainType = CellTerrainType.Plain;

        public int CellFeatureId = -1;      // CellFeatureConfig

        public bool IsInteractable = true;
        
        public CellStates CellStates;
        
        
        public TextMeshProUGUI uiLabel;

        public SquareGridChunk chunk;

        [SerializeField]
        private bool[] EdgePassable = new bool[] {true, true, true, true};
        
        public bool IsPassableEdge(SquareDirection direction)
        {
            return EdgePassable[(int) direction];
        }

        public void InitCell(int index)
        {
            Index = index;
            SetTerrainType(CellTerrainType.Plain);
            CellStates.HasState(CellStates.Interactable);
        }
        
        
        public SquareCell GetNeighbor(SquareDirection squareDirection)
        {
            int x =  Coordinates.X;
            int z =  Coordinates.Z;
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
            return SquareGrid.Instance.Cells[CellCoordinates.ToIndex(x, z)];       
        }
        
        public void SetTerrainType(CellTerrainType terrainType)
        {
            TerrainType = terrainType;
            uiLabel.text = Coordinates + "\n" + TerrainType;
            RefreshChunk();
        }

        private void RefreshChunk()
        {
            chunk?.Refresh();
        }

    }
}