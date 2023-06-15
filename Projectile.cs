using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using AASS.AtlasTexture;

namespace AASS
{
    public class Projectile : GameObject,IBoxCollision
    {
        private Rectangle _atlasProjectile;
        private float _speed;
        public Rectangle Shape {get;set;}
        private float _lifetime;
        private float _angle;

        public Projectile(string name,Rectangle atlasProjectile,
                      Vector2 position,
                      float speed)
        {
            Name = name;
            _atlasProjectile = atlasProjectile;
            Position = position;
            _speed = speed;
            Initialize();
        }
        public override void Initialize()
        {
            IsActive = true;
            
            _angle = MathHelper.ToRadians(90);
            _lifetime = 5.0f;

            UpdateCollision();
        }

        public override void Update(GameTime gameTime)
        {
            UpdateCollision();
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X += _speed * elapsedTime;
            _lifetime -= elapsedTime;
            if(_lifetime < 0) IsActive = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasProjectile),
                                                       Position,_angle,SpriteEffects.None);
            
        }
        //NOTE this will only works correct if _angle is 90 degree
        private void UpdateCollision()
        {
            var center = new Vector2(X + _atlasProjectile.Width / 2,
                                     Y + _atlasProjectile.Height / 2);
                                     var newXY = new Vector2(center.X - _atlasProjectile.Width / 2,
                                                             center.Y - _atlasProjectile.Height / 2);

            Shape = new Rectangle((int)(newXY.X-_atlasProjectile.Height/2),
                                   (int)(newXY.Y-_atlasProjectile.Width/2),
                                   (int)_atlasProjectile.Height,
                                   (int)_atlasProjectile.Width);
        }
    }
}
