
using System;
using System.Numerics;

using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public enum CardinalDirection
{
    North,
    South,
    East,
    West,
}

public static class CardinalDirectionUtils
{
    /// <summary>
    /// Computes the opposite direction of some given direction
    /// </summary>
    /// <param name="direction"></param>
    /// <returns> the oppposite direction </returns>
    /// <exception cref="ArgumentOutOfRangeException"> iff the passed direction is not cardinal</exception>
    public static CardinalDirection Opposite(this CardinalDirection direction)
    {
        return direction switch
        {
            CardinalDirection.North => CardinalDirection.South,
            CardinalDirection.South => CardinalDirection.North,
            CardinalDirection.East => CardinalDirection.West,
            CardinalDirection.West => CardinalDirection.East,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    /// <summary>
    /// Returns this cardinal direction, but as a vector, so that you can
    /// do math on it.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>a vector describing this direction</returns>
    public static Vector2 AsVector(this CardinalDirection direction)
    {
        return direction switch
        {
            CardinalDirection.North => Vector2.up,
            CardinalDirection.South => Vector2.down,
            CardinalDirection.East => Vector2.right,
            CardinalDirection.West => Vector2.left,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}
