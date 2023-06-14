using System.Collections.Generic;

namespace AASS
{
	class CollisionHandler
	{
		class CollideData
		{
			public string NameObj1;
			public string NameObj2;
			public List<ICollisionAction> Actions;
			
			public CollideData(string nameObj1,string nameObj2,
						   	List<ICollisionAction> actions)
			{
				NameObj1 = nameObj1;
				NameObj2 = nameObj2;
				Actions  = actions;
			}
		}
		List<CollideData> collideDatas;
		public CollisionHandler()
		{
			collideDatas = new List<CollideData>();
		}
		
		public void WhenCollide(string nameObj1,string nameObj2,
								List<ICollisionAction> actions)
		{
			//TODO check if already was add obj1 and obj2
			
			collideDatas.Add(new CollideData(nameObj1,nameObj2,actions));
		}
		public void WhenCollide(string nameObj1,string nameObj2, ICollisionAction action)
		{
			List<ICollisionAction> actions = new List<ICollisionAction>();
			actions.Add(action);
			collideDatas.Add(new CollideData(nameObj1,nameObj2,actions));
		}
		public void HandleCollision(Dictionary<string,List<GameObject>> gameObjects)
		{
			foreach(var collideData in collideDatas)
			{
				if(gameObjects.ContainsKey(collideData.NameObj1) &&
			   	gameObjects.ContainsKey(collideData.NameObj2))
				{
					List<GameObject> objs1 = gameObjects[collideData.NameObj1];
					List<GameObject> objs2 = gameObjects[collideData.NameObj2];
					
					foreach(GameObject obj1 in objs1)
					{
						if(!obj1.IsActive) continue;
						foreach(GameObject obj2 in objs2)
						{
							if(obj1 is IBoxCollision c1)
							{
								if(obj2 is IBoxCollision c2)
								{
									if(!obj2.IsActive) continue;
									if(c1.Shape.Intersects(c2.Shape))
									{
										foreach(var action in collideData.Actions)
										{
											action.Execute(obj1,obj2);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
