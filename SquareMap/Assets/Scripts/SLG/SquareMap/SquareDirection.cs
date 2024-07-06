namespace JoyNow.SLG
{
    public enum SquareDirection : byte
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3,
    }
    
    public static class SquareDirectionExtensions
    {
        public static SquareDirection Opposite(this SquareDirection direction)
        {
            return (int)direction < 2 ? (direction + 2) : (direction - 2);
        }
        
        public static SquareDirection Previous(this SquareDirection direction)
        {
            return direction == SquareDirection.North ? SquareDirection.West : (direction - 1);
        }

        public static SquareDirection Next(this SquareDirection direction)
        {
            return direction == SquareDirection.West ? SquareDirection.North : (direction + 1);
        }
    }
}