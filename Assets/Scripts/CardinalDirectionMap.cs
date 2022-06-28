using System;
using JetBrains.Annotations;

namespace DefaultNamespace
{
    public class CardinalDirectionMap<T>
    {
        [ItemCanBeNull] private T[] directions;


        public CardinalDirectionMap(T northValue, T southValue, T eastValue, T westValue)
        {
            directions = new[] {northValue, southValue, eastValue, westValue};
        }

        private static int DirectionToIndex(CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.North => 0,
                CardinalDirection.South => 1,
                CardinalDirection.East =>  2,
                CardinalDirection.West =>  3,
                _ => throw new InvalidCardinalDirectionException(direction),
            };
        }
        
        public void Set(CardinalDirection direction, T value)
        {
            directions[DirectionToIndex(direction)] = value;
        }
        
        
        public T Get(CardinalDirection direction)
        {
            return directions[DirectionToIndex(direction)];
        }

        public T this[CardinalDirection direction]
        {
            get => directions[DirectionToIndex(direction)];
            set => directions[DirectionToIndex(direction)] = value;
        }
    }
}