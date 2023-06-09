using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    public abstract class GameObject
    {
        public Vector2 Position{get;set;}

        public float X { get {return Position.X;}
                         set {Position = new Vector2(value,Position.Y); }
                       }
        public float Y { get {return Position.Y;}
                         set {Position = new Vector2(Position.X,value); }
                       }
        public bool IsActive{get;set;}

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}