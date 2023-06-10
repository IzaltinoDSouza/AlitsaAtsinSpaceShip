namespace AASS
    {
    class MoveUpCommand : ICommand
    {
        private GameObject _gameObject;

        public MoveUpCommand(GameObject gameObject)
        {
            _gameObject = gameObject;
        }
        public void Execute()
        {
            if(_gameObject is IMovementVertical movement)
                movement.MoveUp();
        }
        private void MoveUp()
        {
            Execute();
        }
    }
}