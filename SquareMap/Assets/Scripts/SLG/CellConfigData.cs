using System;

namespace JoyNow.SLG
{
    [Serializable]
    public class CellConfigData
    {
        public int Index;
        
        public CellTerrainType TerrainType;
        
        public int CellFeatureId;
        
        public CellStates CellStates;
    }
}