using UnityEngine;

namespace Snake
{
    public class SnakePlayState : SnakeBaseState
    {
        private SnakeRoot root;
        
        public SnakePlayState(SnakeRoot root)
        {
            this.root = root;
            Debug.Log(root != null);
        }

        public override void Tick()
        {
            root.Body.AdvanceSegments(root.MovementScalar * Time.deltaTime);
        }
    }
}