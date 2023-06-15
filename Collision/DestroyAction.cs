namespace AASS
{
	class DestroyAction : ICollisionAction
	{
		private GameObject _gameObject;
		private string     _name;
		public DestroyAction(GameObject gameObject)
		{
			_gameObject = gameObject;
			_name = null;
		}
		public DestroyAction(string name)
		{
			_name = name;
			_gameObject = null;
		}
		public void Execute(GameObject obj1,GameObject obj2)
		{
			if(_name != null && _name == obj1.Name)
			{
				obj1.IsActive = false;
			}
			if(_name != null && _name == obj2.Name)
			{
				obj2.IsActive = false;
			}
			if(_gameObject != null)
			{
				_gameObject.IsActive = false;
			}
		}
    }
}
