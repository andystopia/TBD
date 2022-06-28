
using System;

public class InvalidCardinalDirectionException : Exception
{
    public InvalidCardinalDirectionException(CardinalDirection direction) : 
        base($"The possible cardinal directions are inherently " +
             $"North, South, East, and West, but the program generated a cardinal " +
             $"direction with the value of {direction}. Because cardinal directions " +
             $"are an invariant closed set, this is a sign of a serious bug, " +
             $"please check the call stack and resolve the problem.")
    {
        
    }
}