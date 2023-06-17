namespace AASS
{
	class ShieldEnableAction : ICollisionAction
	{
		private GameObject _gameObject;
		private string     _name;
		public ShieldEnableAction(GameObject gameObject)
		{
			_gameObject = gameObject;
			_name = null;
		}
		public ShieldEnableAction(string name)
		{
			_name = name;
			_gameObject = null;
		}
		public void Execute(GameObject obj1,GameObject obj2)
		{
			if(_name != null && _name == obj1.Name && obj1 is IShield s1)
			{
				if(s1.ShieldEnable)
                {
                    if(s1.ShieldCount < s1.ShieldMaxCount)
                        s1.ShieldCount += 1;
                }else
                {
                    s1.ShieldCount = 1;
					s1.ShieldEnable = true;
                }
			}
			if(_name != null && _name == obj2.Name && obj2 is IShield s2)
			{
				if(s2.ShieldEnable)
                {
                    if(s2.ShieldCount < s2.ShieldMaxCount)
                        s2.ShieldCount += 1;
                }else
                {
                    s2.ShieldCount = 1;
					s2.ShieldEnable = true;
                }
			}
			if(_gameObject != null && _gameObject is IShield s3)
			{
				if(s3.ShieldEnable)
                {
                    if(s3.ShieldCount < s3.ShieldMaxCount)
                        s3.ShieldCount += 1;
                }else
                {
                    s3.ShieldCount = 1;
					s3.ShieldEnable = true;
                }
			}
		}
    }
}
