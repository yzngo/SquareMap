using System;

namespace JoyNow.SLG
{
    [Serializable]
    public class GridConfigData
    {
        public int ChunkCountX;
        
        public int ChunkCountZ;
        
        public CellConfigData[] CellConfigDatas;
    }
}