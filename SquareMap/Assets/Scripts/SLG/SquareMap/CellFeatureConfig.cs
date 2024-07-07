namespace JoyNow.SLG
{
    public enum FeatureType
    {
        Sprite,
        Spine,
    }
    public class CellFeatureConfig
    {
        public int Id;
        public FeatureType FeatureType;
        public string FeatureName;
        public int Layer;
    }
}