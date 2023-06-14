using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using AASS.AtlasTexture;

namespace AASS
{
    public class Meteor : GameObject,IHealth,IBoxCollision
    {
        private Rectangle _atlasMeteor;
        private float _speed;
        public Rectangle Shape {get;set;}
        public int MaxHealth {get;set;}
        public int CurrentHealth {get;set;}

        public Meteor(Rectangle atlasMeteor,
                      Vector2 position,
                      float speed)
        {
            _atlasMeteor = atlasMeteor;
            Position = position;
            _speed = speed;
        }
        public override void Initialize()
        {
            Shape = new Rectangle((int)X,(int)Y,_atlasMeteor.Width,_atlasMeteor.Height);
            CurrentHealth = 100;
            MaxHealth = 100;
        }

        public override void Update(GameTime gameTime)
        {
            Shape = new Rectangle((int)X,(int)Y,_atlasMeteor.Width,_atlasMeteor.Height);
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X -= _speed * elapsedTime;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasMeteor),
                                                       Position);
        
        }
        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            if(CurrentHealth < 0)
            {
                CurrentHealth = 0;
                IsActive = false;
            }
        }
        public void Heal(int amount)
        {
            //Nothing
        }
    }
}