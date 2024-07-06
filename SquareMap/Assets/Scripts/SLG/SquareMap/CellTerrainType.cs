using System;
using UnityEngine;

namespace JoyNow.SLG
{
    public enum CellTerrainType : byte
    {
        Default = 1,
        Land = 2,
        Sea = 3,
    }
    
    
    public static class CellTerrainTypeExtension
    {
        public static Color GetColorTip(this CellTerrainType terrainType)
        {
            Color color = Color.white;
            switch (terrainType)
            {
                case CellTerrainType.Land:
                    color = Color.yellow;
                    break;
                case CellTerrainType.Sea:
                    color = Color.cyan;
                    break;
            }
            color.a = 0.5f;
            return color;
        }
    }
}