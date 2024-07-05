﻿namespace JoyNow.SLG
{
    public enum SquareDirection
    {
        North,
        East,
        South,
        West
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