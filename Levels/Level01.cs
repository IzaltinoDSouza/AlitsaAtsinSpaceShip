using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class Level01 : Level
    {
        private BackgroundScrollable _background;
        private CollisionHandler _collision;
        private Universe _universe;
        private float _levelCountdownTime;
        public override void Initialize(int level,SpaceShip alitsa,SpaceShip atsin)
        {
            _background = new BackgroundScrollable(Vector2.Zero,50.0f);
            
            var tinyMeteorVariations = new List<Rectangle>();
            tinyMeteorVariations.Add(new Rectangle(346,814,18,18));
            tinyMeteorVariations.Add(new Rectangle(399,814,16,15));
            var _meteorTinyWave = new MeteorWave("TinyMeteor",tinyMeteorVariations,2,6,70f,5.0f);

            var meteorWaves = new List<MeteorWave>();
            meteorWaves.Add(_meteorTinyWave);

            _collision = new CollisionHandler();

            CollisionActions(alitsa);
            CollisionActions(atsin);

            _universe = new Universe(_background,_collision,meteorWaves);

            _universe.AddGameObject(alitsa.Name,alitsa);
            _universe.AddGameObject(atsin.Name,atsin);
            _universe.AddGameObject("TinyMeteor",new List<GameObject>());

            _levelCountdownTime = 2*60;
        }

        public override void LoadContent(ContentManager content)
        {
            _background.SetBackground(content.Load<Texture2D>("Background"));
        }

        public override bool Update(GameTime gameTime)
        {
            _universe.Update(gameTime);
            _levelCountdownTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                                   Color.White);
        }
        private void CollisionActions(GameObject player)
        {
            //TinyMeteor And Alitsa collide
            var MeteorAndAlitsa = new List<ICollisionAction>();
            MeteorAndAlitsa.Add(new HealthAction(player.Name,-1));
            MeteorAndAlitsa.Add(new DestroyAction("TinyMeteor"));
            _collision.WhenCollide("TinyMeteor",player.Name,MeteorAndAlitsa);

            //Alista projectile and TinyMeteor collide
            var AlitsaProjectileAndMeteor = new List<ICollisionAction>();
            AlitsaProjectileAndMeteor.Add(new DestroyAction(player.Name+"Projectile"));
            AlitsaProjectileAndMeteor.Add(new DestroyAction("TinyMeteor"));
            AlitsaProjectileAndMeteor.Add(new ScoreAction(player,+1));
            _collision.WhenCollide(player.Name+"Projectile","TinyMeteor",AlitsaProjectileAndMeteor);
        }
    }
}