namespace AASS
{
    class ShootCommand : ICommand
    {
        private GameObject _gameObject;

        public ShootCommand(GameObject gameObject)
        {
            _gameObject = gameObject;
        }
        public void Execute()
        {
            if(_gameObject is IShoot s)
                s.Shoot = true;
        }
    }
}
