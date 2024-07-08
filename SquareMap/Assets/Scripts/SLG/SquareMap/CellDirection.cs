namespace JoyNow.SLG
{
    public enum CellDirection : byte
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
    }
    
    public static class SquareDirectionExtensions
    {
        public static CellDirection Opposite(this CellDirection direction)
        {
            return (int)direction < 2 ? (direction + 2) : (direction - 2);
        }
        
        public static CellDirection Previous(this CellDirection direction)
        {
            return direction == CellDirection.North ? CellDirection.West : (direction - 1);
        }

        public static CellDirection Next(this CellDirection direction)
        {
            return direction == CellDirection.West ? CellDirection.North : (direction + 1);
        }
    }
}