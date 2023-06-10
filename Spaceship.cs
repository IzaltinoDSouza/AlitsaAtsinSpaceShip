using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class SpaceShip : GameObject,IMovementVertical
    {
        private Rectangle _atlasSpaceship;
        float _moveDirection;
        float _speed;

        float _angle;
        public SpaceShip(Rectangle atlasSpaceship)
        {
            _atlasSpaceship = atlasSpaceship;
            Position = Vector2.Zero;
            IsActive = true;
        }
        public SpaceShip(Rectangle atlasSpaceship,Vector2 position)
        {
            _atlasSpaceship = atlasSpaceship;
            Position = position;
            IsActive = true;
        }
        public override void Initialize()
        {
            _moveDirection = 0;
            _speed = 100;
            _angle = MathHelper.ToRadians(90);
        }
        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_moveDirection <= -1)
            {
                Y -= _speed * elapsedTime;
                _moveDirection = 0;
            }
            else if(_moveDirection >= 1)
            {
                Y +=  _speed * elapsedTime;
                _moveDirection = 0;
            }

            
            if(Y < _atlasSpaceship.Height)
            {
                Y = _atlasSpaceship.Height;
            }
            if(Y > Global.ScreenHeight - _atlasSpaceship.Height)
            {
                Y = Global.ScreenHeight - _atlasSpaceship.Height;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasSpaceship),
                                                            Position,_angle,
                                                            SpriteEffects.FlipVertically);
        }
        public void MoveUp()
        {
            _moveDirection = -1;
        }
        public void MoveDown()
        {
            _moveDirection = +1;
        }
    }
}