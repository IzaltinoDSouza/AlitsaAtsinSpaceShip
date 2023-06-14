namespace AASS
{
	class IHealthAction : ICollisionAction
	{
		private GameObject _gameObject;
		private string     _name;
		private int 	   _healthAmount;
		public IHealthAction(GameObject gameObject,int healthAmount)
		{
			_gameObject = gameObject;
			_name = null;
			_healthAmount = healthAmount;
		}
		public IHealthAction(string name,int healthAmount)
		{
			_name = name;
			_gameObject = null;
			_healthAmount = healthAmount;
		}
		public void Execute(GameObject obj1,GameObject obj2)
		{
			/*
				when collide obj1 and obj2 do action on obj1 if it has name
				when collide obj1 and obj2 do action on obj2 if it has name 
				when collide obj1 and obj2 do action on other object
			*/
			if(_name != null && _name == obj1.Name && obj1 is IHealth health1)
			{
				if(_healthAmount > 0)
					health1.Heal(_healthAmount);
				else
					health1.TakeDamage(_healthAmount*-1);
			}
			if(_name != null && _name == obj2.Name &&  obj2 is IHealth health2)
			{
				if(_healthAmount > 0)
					health2.Heal(_healthAmount);
				else
					health2.TakeDamage(_healthAmount*-1);
			}
			if(_gameObject != null && _gameObject is IHealth health3)
			{
				if(_healthAmount > 0)
					health3.Heal(_healthAmount);
				else
					health3.TakeDamage(_healthAmount*-1);
			}
		}
    }
}
