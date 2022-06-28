using GameLayout;
using Unity.VisualScripting;
using UnityEngine;

namespace SnakeSegment
{
    [RequireComponent(typeof(SnapBehavior))]
    public class SnakeSegmentRoot : MonoBehaviour
    {
        [SerializeField] private CardinalDirection heading = CardinalDirection.East;

        public CardinalDirection Heading => heading;

        public void Move(float amount)
        {
            transform.position += (Vector3) (heading.AsVector() * amount);
        }

        public bool IsNearGridCellCenter(GameGridLayout layout, float epsilon = 0.05f)
        {
            var nearest = layout.GetNearestGridCenter(Position);
            var distance = Vector2.Distance(nearest, Position);
            return distance <= epsilon;
        }

        public Vector2 Position => transform.position;
    }
}