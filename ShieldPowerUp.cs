using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class ShieldPowerUp : GameObject,IBoxCollision
    {
        private Rectangle _atlasShieldPowerUp;
        public Rectangle Shape{get;set;}
        private Vector2 _position;
        private float _speed;
        public ShieldPowerUp(Vector2 position,float speed)
        {
            _position = position;
            _speed = speed;
           
            Initialize();
        }
        public override void Initialize()
        {
            _atlasShieldPowerUp = new Rectangle(797,143,30,30);

            Shape = new Rectangle((int)X-_atlasShieldPowerUp.Width/2,
            					  (int)Y-_atlasShieldPowerUp.Height/2,
            					  _atlasShieldPowerUp.Width,
            					  _atlasShieldPowerUp.Height);
            IsActive = true;
            Name = "ShieldPowerUp";
        }
        public override void Update(GameTime gameTime)
        {
            Shape = new Rectangle((int)X-_atlasShieldPowerUp.Width/2,
            					  (int)Y-_atlasShieldPowerUp.Height/2,
            					  _atlasShieldPowerUp.Width,
            					  _atlasShieldPowerUp.Height);

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X -= _speed * elapsedTime;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasShieldPowerUp),
                                                       Position);
            
        }
    }
}
