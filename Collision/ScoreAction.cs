namespace AASS
{
	class ScoreAction : ICollisionAction
	{
		private GameObject _gameObject;
		private string     _name;
		private int 	   _scoreAmount;
		public ScoreAction(GameObject gameObject,int scoreAmount)
		{
			_gameObject = gameObject;
			_name = null;
			_scoreAmount = scoreAmount;
		}
		public ScoreAction(string name,int scoreAmount)
		{
			_name = name;
			_gameObject = null;
			_scoreAmount = scoreAmount;
		}
		public void Execute(GameObject obj1,GameObject obj2)
		{
			/*
				when collide obj1 and obj2 do action on obj1 if it has name
				when collide obj1 and obj2 do action on obj2 if it has name 
				when collide obj1 and obj2 do action on other object
			*/
			if(_name != null && _name == obj1.Name && obj1 is IScore score1)
			{
				if(_scoreAmount > 0)
					score1.Score += _scoreAmount;
				else
					score1.Score -= _scoreAmount*-1;
			}
			if(_name != null && _name == obj2.Name &&  obj2 is IScore score2)
			{
				if(_scoreAmount > 0)
					score2.Score += _scoreAmount;
				else
					score2.Score -= _scoreAmount*-1;
			}
			if(_gameObject != null && _gameObject is IScore score3)
			{
				if(_scoreAmount > 0)
					score3.Score += _scoreAmount;
				else
					score3.Score -= _scoreAmount*-1;
			}
		}
    }
}
