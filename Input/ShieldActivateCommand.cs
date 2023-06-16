namespace AASS
{
    class ShieldActivateCommand : ICommand
    {
        private GameObject _gameObject;

        public ShieldActivateCommand(GameObject gameObject)
        {
            _gameObject = gameObject;
        }
        public void Execute()
        {
            if(_gameObject is IShield s)
            {
                if(s.ShieldEnable)
                {
                    s.ShieldActivate = true;
                }
            }
        }
    }
}
