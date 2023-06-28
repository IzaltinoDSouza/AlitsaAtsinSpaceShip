using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class Level10 : Level
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
            var tinyMeteorWave = new MeteorWave("TinyMeteor",tinyMeteorVariations,2,2,70f,15.0f);

            //Small Meteor
            var smallMeteorVariations = new List<Rectangle>();
            smallMeteorVariations.Add(new Rectangle(406,234,28,28));
            smallMeteorVariations.Add(new Rectangle(778,587,29,26));
            var smallMeteorWave = new MeteorWave("SmallMeteor",smallMeteorVariations,3,4,85f,12.0f);

            //Med Meteor
            var medMeteorVariations = new List<Rectangle>();
            medMeteorVariations.Add(new Rectangle(651,447,43,43));
            medMeteorVariations.Add(new Rectangle(237,452,45,40));
            var medMeteorWave = new MeteorWave("MedMeteor",medMeteorVariations,6,8,100f,6.0f);

            //Big Meteor
            var bigMeteorVariations = new List<Rectangle>();
            bigMeteorVariations.Add(new Rectangle(224,664,101,84));
            bigMeteorVariations.Add(new Rectangle(0,520,120,98));
            bigMeteorVariations.Add(new Rectangle(518,810,89,82));
            bigMeteorVariations.Add(new Rectangle(327,452,98,96));
            var bigMeteorWave = new MeteorWave("BigMeteor",bigMeteorVariations,1,2,125f,10.0f);


            var meteorWaves = new List<MeteorWave>();
            meteorWaves.Add(tinyMeteorWave);
            meteorWaves.Add(smallMeteorWave);
            meteorWaves.Add(medMeteorWave);
            meteorWaves.Add(bigMeteorWave);

            _collision = new CollisionHandler();

            CollisionActions(alitsa);
            CollisionActions(atsin);

            _universe = new Universe(_background,_collision,meteorWaves,new ShieldPowerUpWave(60f,_levelCountdownTime/8));

            _universe.AddGameObject(alitsa.Name,alitsa);
            _universe.AddGameObject(atsin.Name,atsin);
            _universe.AddGameObject("TinyMeteor",new List<GameObject>());
            _universe.AddGameObject("SmallMeteor",new List<GameObject>());
            _universe.AddGameObject("MedMeteor",new List<GameObject>());
            _universe.AddGameObject("BigMeteor",new List<GameObject>());
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

            //SmallMeteor and Player collide
            var SmallMeteorAndPlayer = new List<ICollisionAction>();
            SmallMeteorAndPlayer.Add(new HealthAction(player.Name,-5));
            SmallMeteorAndPlayer.Add(new DestroyAction("SmallMeteor"));
            SmallMeteorAndPlayer.Add(new UserDefinedAction((GameObject obj1,GameObject obj2) => {
                if(obj2 is SpaceShip spaceship)
                {
                    if(!spaceship.ShieldActivate)
                    {
                        Global.SFXSounds["MeteorCollide"].Stop();
                        Global.SFXSounds["MeteorCollide"].Play();
                    }
                }
            }));
            _collision.WhenCollide("SmallMeteor",player.Name,SmallMeteorAndPlayer);

            //MedMeteor and Player collide
            var medMeteorAndPlayer = new List<ICollisionAction>();
            medMeteorAndPlayer.Add(new HealthAction(player.Name,-8));
            medMeteorAndPlayer.Add(new DestroyAction("MedMeteor"));
            medMeteorAndPlayer.Add(new UserDefinedAction((GameObject obj1,GameObject obj2) => {
                if(obj2 is SpaceShip spaceship)
                {
                    if(!spaceship.ShieldActivate)
                    {
                        Global.SFXSounds["MeteorCollide"].Stop();
                        Global.SFXSounds["MeteorCollide"].Play();
                    }
                }
            }));
            _collision.WhenCollide("MedMeteor",player.Name,medMeteorAndPlayer);

            //BigMeteor and Player collide
            var bigMeteorAndPlayer = new List<ICollisionAction>();
            bigMeteorAndPlayer.Add(new DestroyAction(player.Name));
            bigMeteorAndPlayer.Add(new DestroyAction("BigMeteor"));
            bigMeteorAndPlayer.Add(new UserDefinedAction((GameObject obj1,GameObject obj2) => {
                if(obj2 is SpaceShip spaceship)
                {
                    if(!spaceship.ShieldActivate)
                    {
                        Global.SFXSounds["MeteorCollide"].Stop();
                        Global.SFXSounds["MeteorCollide"].Play();
                    }
                    if(spaceship.ShieldEnable)
                    {
                        spaceship.IsActive = true; //if has shield need to redo the previous destroy action 
                        spaceship.ShieldActivate = false;
                        spaceship.ShieldCount -= 1;
                    }
                }
            }));
            _collision.WhenCollide("BigMeteor",player.Name,bigMeteorAndPlayer);

            //Player projectile and TinyMeteor collide
            var playerProjectileAndTinyMeteor = new List<ICollisionAction>();
            playerProjectileAndTinyMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            playerProjectileAndTinyMeteor.Add(new DestroyAction("TinyMeteor"));
            playerProjectileAndTinyMeteor.Add(new ScoreAction(player,+1));
            playerProjectileAndTinyMeteor.Add(new PlaySFXSoundAction("ProjectileCollide"));
            _collision.WhenCollide(player.Name+"Projectile","TinyMeteor",playerProjectileAndTinyMeteor);

            //Player projectile and SmallMeteor collide
            var playerProjectileAndSmallMeteor = new List<ICollisionAction>();
            playerProjectileAndSmallMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            playerProjectileAndSmallMeteor.Add(new HealthAction("SmallMeteor",-45));
            playerProjectileAndSmallMeteor.Add(new ScoreAction(player,+3));
            playerProjectileAndSmallMeteor.Add(new PlaySFXSoundAction("ProjectileCollide"));
            _collision.WhenCollide(player.Name+"Projectile","SmallMeteor",playerProjectileAndSmallMeteor);
            
            //Player projectile and MedMeteor collide
            var playerProjectileAndMedMeteor = new List<ICollisionAction>();
            playerProjectileAndMedMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            playerProjectileAndMedMeteor.Add(new HealthAction("MedMeteor",-33));
            playerProjectileAndMedMeteor.Add(new ScoreAction(player,+6));
            playerProjectileAndMedMeteor.Add(new PlaySFXSoundAction("ProjectileCollide"));
            _collision.WhenCollide(player.Name+"Projectile","MedMeteor",playerProjectileAndMedMeteor);

            //Player projectile and BigMeteor collide
            var playerProjectileAndBigMeteor = new List<ICollisionAction>();
            playerProjectileAndBigMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            playerProjectileAndBigMeteor.Add(new HealthAction("BigMeteor",-10));
            playerProjectileAndBigMeteor.Add(new ScoreAction(player,+18));
            playerProjectileAndBigMeteor.Add(new PlaySFXSoundAction("ProjectileCollide"));
            _collision.WhenCollide(player.Name+"Projectile","BigMeteor",playerProjectileAndBigMeteor);

            //ShieldPowerUp and Player
            var shieldPowerUpAndPlayer = new List<ICollisionAction>();
            shieldPowerUpAndPlayer.Add(new ShieldEnableAction(player.Name));
            shieldPowerUpAndPlayer.Add(new DestroyAction("ShieldPowerUp"));
            _collision.WhenCollide("ShieldPowerUp",player.Name,shieldPowerUpAndPlayer);
        }
    }
}
