namespace Snake
{
    public class SnakeStateFactory
    {
        private SnakeRoot _snakeRoot;

        public SnakeStateFactory(SnakeRoot snakeRoot)
        {
            _snakeRoot = snakeRoot;
        }

        public SnakePlayState MakePlayState() => new SnakePlayState();
    }
}