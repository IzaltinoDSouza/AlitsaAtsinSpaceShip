namespace AASS
{
    class MoveDownCommand : ICommand
    {
        private GameObject _gameObject;

        public MoveDownCommand(SpaceShip gameObject)
        {
            _gameObject = gameObject;
        }
        public void Execute()
        {
            if(_gameObject is IMovementVertical movement)
                movement.MoveDown();
        }
        private void MoveDown()
        {
            Execute();
        }
    }
}