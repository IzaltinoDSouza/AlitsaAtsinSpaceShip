using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class SpaceShip : GameObject,IMovementVertical,IHealth,IBoxCollision
    {
        private Rectangle _atlasSpaceship;
        float _moveDirection;
        float _speed;

        float _angle;

        public int MaxHealth {get;set;}
        public int CurrentHealth {get;set;}
        public Rectangle Shape { get; set; }
        public SpaceShip(string name,Rectangle atlasSpaceship)
        {
            Name = name;
            _atlasSpaceship = atlasSpaceship;
            Position = Vector2.Zero;
            IsActive = true;
        }
        public SpaceShip(string name,Rectangle atlasSpaceship,Vector2 position)
        {
            Name = name;
            _atlasSpaceship = atlasSpaceship;
            Position = position;
            IsActive = true;
        }
        public override void Initialize()
        {
            MaxHealth = 100;
            CurrentHealth = 100;

            _moveDirection = 0;
            _speed = 100;
            _angle = MathHelper.ToRadians(90);
            
            Shape = new Rectangle((int)X,(int)Y,_atlasSpaceship.Width,_atlasSpaceship.Height);
        }
        public override void Update(GameTime gameTime)
        {
            Shape = new Rectangle((int)X,(int)Y,_atlasSpaceship.Width,_atlasSpaceship.Height);
            
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
        public void TakeDamage(int amount)
        {
        	CurrentHealth -= amount;
        	if(CurrentHealth < 0)
            {
        		CurrentHealth = 0;
                IsActive = false;
            }
            System.Console.WriteLine($"TakeDamage : {amount} | Health : {CurrentHealth} ");
        }
        public void Heal(int amount)
        {
        	CurrentHealth += amount;
        	if(CurrentHealth > MaxHealth)
        		CurrentHealth = MaxHealth;
        }
    }
}
