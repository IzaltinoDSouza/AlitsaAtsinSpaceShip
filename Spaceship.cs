using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class SpaceShip : GameObject,IMovementVertical,IShoot,IHealth,IBoxCollision,ICreateProjectile,IShield
    {
        private Rectangle _atlasSpaceship;
        private Rectangle _atlasShield;
        float _moveDirection;
        float _speed;

        float _angle;

        public int MaxHealth {get;set;}
        public int CurrentHealth {get;set;}
        public Rectangle Shape { get; set; }
        public bool Shoot { get; set; }

        private float _nextShootTime;
        private float _nextShootTimeDelay;

        public bool ShieldEnable{get;set;}
        public bool ShieldActivate{get;set;}
        public float ShieldDuration{get;set;}
        public int ShieldMaxCount{get;set;}
        public int ShieldCount{get;set;}
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
            
            Shape = new Rectangle((int)X-_atlasSpaceship.Width/2,
            					  (int)Y-_atlasSpaceship.Height/2,
            					  _atlasSpaceship.Width,
            					  _atlasSpaceship.Height);
            _nextShootTime = 0.0f;
            _nextShootTimeDelay = 0.25f;

            ShieldEnable = true;//false;
            ShieldActivate = false;
            ShieldDuration =  20.0f;
            ShieldMaxCount = 3;
            ShieldCount = 0;

            _atlasShield = new Rectangle(0,412,133,108);
        }
        public override void Update(GameTime gameTime)
        {            
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(Shoot)
            {
                _nextShootTime -= elapsedTime;
                if(_nextShootTime < 0.0f)
                    _nextShootTime = 0.0f;
            }
            if(ShieldEnable && ShieldActivate)
            {
                ShieldDuration -= elapsedTime;
                if(ShieldDuration <= 0.0f)
                {
                    ShieldActivate = false;
                    ShieldCount -= 1;
                    if(ShieldCount <= 0)
                        ShieldEnable = false;
                }
                Shape = new Rectangle((int)X-_atlasShield.Width/2,
                                     (int)Y-_atlasShield.Height/2,
                                     _atlasShield.Width,
                                     _atlasShield.Height);
            }else
            {
                Shape = new Rectangle((int)X-_atlasSpaceship.Width/2,
                                     (int)Y-_atlasSpaceship.Height/2,
                                     _atlasSpaceship.Width,
                                     _atlasSpaceship.Height);
            }

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
            if(ShieldEnable && ShieldActivate)
            {
                AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                                        new AtlasTexture2D(0,_atlasShield),
                                                        new Vector2(X+5,Y),_angle,
                                                        SpriteEffects.None);
            }
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
            if(ShieldEnable && ShieldActivate) return;

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
        public Projectile CreateProjectile()
        {
            if(Shoot && _nextShootTime <= 0.0f)
            {
                _nextShootTime = _nextShootTimeDelay;
                Shoot = false;
                return new Projectile("Projectile"+Name,
                                      new Rectangle(856,421,9,54),
                                      new Vector2(X +_atlasSpaceship.Width/2,Y),
                                      250f);
            }else
            {
                return null;
            }
        }
    }
}
