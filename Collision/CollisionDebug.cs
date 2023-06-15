using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
	class CollisionDebug
	{
		private Color _color = Color.Red;
		public void Draw(SpriteBatch spriteBatch,List<GameObject> gameObjects)
		{
			foreach(var gameObject in gameObjects)
			{
				if(gameObject.IsActive)
				{
					if(gameObject is IBoxCollision boxCollision)
						DrawBoxCollision(spriteBatch,boxCollision);
				}
			}
		}
		public void Draw(SpriteBatch spriteBatch,GameObject gameObject)
		{
			if(gameObject is IBoxCollision boxCollision)
				DrawBoxCollision(spriteBatch,boxCollision);
		}
		private void DrawBoxCollision(SpriteBatch spriteBatch,IBoxCollision boxCollision)
		{
    		var top = new Rectangle(boxCollision.Shape.X, boxCollision.Shape.Y,
    								boxCollision.Shape.Width, 1);

    		var right = new Rectangle(boxCollision.Shape.X + boxCollision.Shape.Width - 1,
    								  boxCollision.Shape.Y, 1, boxCollision.Shape.Height);

    		var bottom = new Rectangle(boxCollision.Shape.X,
    								   boxCollision.Shape.Y + boxCollision.Shape.Height - 1,
    								   boxCollision.Shape.Width, 1);
    		
    		var left = new Rectangle(boxCollision.Shape.X,
    								 boxCollision.Shape.Y, 1, boxCollision.Shape.Height);
    		
			spriteBatch.Draw(Global.TextureDebug,top,_color);
			
			spriteBatch.Draw(Global.TextureDebug,right,_color);
			
			spriteBatch.Draw(Global.TextureDebug,bottom,_color);
			
			spriteBatch.Draw(Global.TextureDebug,left,_color);
		}
	}
}
