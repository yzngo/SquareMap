namespace JoyNow.SLG
{
    public enum FeatureAssetType
    {
        Sprite,
        Spine,
    }
    
    public class CellFeatureConfig
    {
        public int Id;
        public FeatureAssetType FeatureAssetType;
        public string FeatureName;
        public bool IsPassable = true;
        public int Layer;
    }
}