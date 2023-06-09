using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class Level01 : Level
    {
        private float _levelCountdownTime;
        private Color _levelCountdownColor;
        private BackgroundScrollable _background;
        private CollisionHandler _collision;
        private Universe _universe;
        public override void Initialize(int level,SpaceShip alitsa,SpaceShip atsin)
        {
            _levelCountdownTime = 2*60;
            _levelCountdownColor = Color.White;
            _background = new BackgroundScrollable(Vector2.Zero,50.0f);
            
            //Tiny Meteor
            var tinyMeteorVariations = new List<Rectangle>();
            tinyMeteorVariations.Add(new Rectangle(346,814,18,18));
            tinyMeteorVariations.Add(new Rectangle(399,814,16,15));
            var tinyMeteorWave = new MeteorWave("TinyMeteor",tinyMeteorVariations,2,6,70f,5.0f);

            var meteorWaves = new List<MeteorWave>();
            meteorWaves.Add(tinyMeteorWave);

            _collision = new CollisionHandler();

            CollisionActions(alitsa);
            CollisionActions(atsin);

            _universe = new Universe(_background,_collision,meteorWaves,new ShieldPowerUpWave(60f,_levelCountdownTime/2));

            _universe.AddGameObject(alitsa.Name,alitsa);
            _universe.AddGameObject(atsin.Name,atsin);
            _universe.AddGameObject("TinyMeteor",new List<GameObject>());
            _universe.AddGameObject("ShieldPowerUp",new List<GameObject>());
        }

        public override void LoadContent(ContentManager content)
        {
            _background.SetBackground(content.Load<Texture2D>("Background/DarkPurple"));
        }

        public override bool Update(GameTime gameTime)
        {
            _universe.Update(gameTime);
            _levelCountdownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_levelCountdownTime <= 10.0f && _levelCountdownTime >= 0.1f)
            {
                Global.SFXSounds["Timeout"].Play();
                _levelCountdownColor = Color.OrangeRed;
            }
            if(_levelCountdownTime <= 0.0f) return false;
            return true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _universe.Draw(spriteBatch);
            var textCountdownTime = _levelCountdownTime.ToString("0.00");
            var textSizeCountdownTime = Global.DefaultFont.MeasureString(textCountdownTime);
            spriteBatch.DrawString(Global.DefaultFont,
                                   textCountdownTime,
                                   new Vector2((Global.ScreenWidth-textSizeCountdownTime.X)/2,
                                               textSizeCountdownTime.Y),
                                   _levelCountdownColor);
        }
        private void CollisionActions(GameObject player)
        {
            //TinyMeteor and Player collide
            var tinyMeteorAndPlayer = new List<ICollisionAction>();
            tinyMeteorAndPlayer.Add(new HealthAction(player.Name,-1));
            tinyMeteorAndPlayer.Add(new DestroyAction("TinyMeteor"));
            tinyMeteorAndPlayer.Add(new UserDefinedAction((GameObject obj1,GameObject obj2) => {
                if(obj2 is SpaceShip spaceship)
                {
                    if(!spaceship.ShieldActivate)
                    {
                        Global.SFXSounds["MeteorCollide"].Stop();
                        Global.SFXSounds["MeteorCollide"].Play();
                    }
                }
            }));
            _collision.WhenCollide("TinyMeteor",player.Name,tinyMeteorAndPlayer);

            //Player projectile and TinyMeteor collide
            var playerProjectileAndTinyMeteor = new List<ICollisionAction>();
            playerProjectileAndTinyMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            playerProjectileAndTinyMeteor.Add(new DestroyAction("TinyMeteor"));
            playerProjectileAndTinyMeteor.Add(new ScoreAction(player,+1));
            playerProjectileAndTinyMeteor.Add(new PlaySFXSoundAction("ProjectileCollide"));
            _collision.WhenCollide(player.Name+"Projectile","TinyMeteor",playerProjectileAndTinyMeteor);
            
            //ShieldPowerUp and Player
            var shieldPowerUpAndPlayer = new List<ICollisionAction>();
            shieldPowerUpAndPlayer.Add(new ShieldEnableAction(player.Name));
            shieldPowerUpAndPlayer.Add(new DestroyAction("ShieldPowerUp"));
            _collision.WhenCollide("ShieldPowerUp",player.Name,shieldPowerUpAndPlayer);
        }
    }
}
