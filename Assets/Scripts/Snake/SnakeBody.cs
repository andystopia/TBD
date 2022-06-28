using System;
using System.Collections.Generic;
using System.Linq;
using GameLayout;
using SnakeSegment;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Snake
{
    [Serializable]
    public class SnakeBody
    {
        [SerializeReference] private List<SnakeSegmentRoot> segments;

        public void AdvanceSegments(float amount)
        {
            foreach (var segment in segments)
            {
                   segment.Move(amount);
            }
        }

        
        public void UpdateDirections(GameGridLayout layout, float epsilon = 0.05f)
        {
            // https://stackoverflow.com/questions/577590/pair-wise-iteration-in-c-sharp-or-sliding-window-enumerator
            var segment_window = segments.Zip(segments.Skip(1), Tuple.Create);

            foreach (var (current, next) in segment_window)
            {
                var currentPosition = current.Position;
                var nearest = layout.GetNearestGridCenter(currentPosition);
                var distance = Vector2.Distance(nearest, currentPosition);

                if (distance < epsilon)
                {
                    
                }

            }
        }
    }
}