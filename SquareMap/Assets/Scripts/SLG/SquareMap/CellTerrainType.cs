using System;
using UnityEngine;

namespace JoyNow.SLG
{
    // 地形类型，需要迁移到数据表，定义是否通行，移动力等
    // 在地图上仅标注格子类型，具体的显示效果使用 Tilemap 实现
    public enum CellTerrainType : byte
    {
        Plain = 0,  // 默认，平原，无特殊效果
        Water,
        Lawn,  // 草地，无特殊效果
        Mountain,  // 高山，无法通行
        Building,  // 建筑，无法通行
        Forest, // 林地，通行力消耗翻倍
    }
    
    
    public static class CellTerrainTypeExtension
    {
        public static Color GetColorTip(this CellTerrainType terrainType)
        {
            Color color = terrainType switch
            {
                CellTerrainType.Plain => Color.gray,
                CellTerrainType.Water => Color.cyan,
                CellTerrainType.Lawn => Color.green,
                CellTerrainType.Mountain => Color.white,
                CellTerrainType.Building => Color.yellow,
                CellTerrainType.Forest => new Color(0, 0.5f, 0, 1),
                _ => Color.black,
            };
            color.a = 1f;
            return color;
        }
        
        
        public static string GetName(this CellTerrainType terrainType)
        {
            string name = terrainType switch
            {
                CellTerrainType.Plain => "平原",
                CellTerrainType.Water => "水面",
                CellTerrainType.Lawn => "草地",
                CellTerrainType.Mountain => "山脉",
                CellTerrainType.Building => "建筑",
                CellTerrainType.Forest => "森林",
                _ => ""
            };
            return name;
        }
    }
}