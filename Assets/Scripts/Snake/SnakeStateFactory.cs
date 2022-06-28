using UnityEngine;

namespace Snake
{
    public class SnakeStateFactory : MonoBehaviour
    {
        private SnakeRoot _snakeRoot;


        public void Awake()
        {
            _snakeRoot = GetComponent<SnakeRoot>();
        }

        public SnakePlayState MakePlayState() => new(_snakeRoot);
    }
}