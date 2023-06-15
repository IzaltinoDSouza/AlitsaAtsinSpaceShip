using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AASS
{
    public class Universe 
    {
        private Texture2D _backgroundTexture;
        private Vector2 _backgroundPosition;
        private float _backgroundScrollSpeed;
        private Dictionary<string,List<GameObject>> _gameObjects;
        private SpaceShip _alitsa;
        private SpaceShip _atsin;
        private InputHandler _input;
        private CollisionHandler _collision;
        private CollisionDebug   _collisionDebug;

        private Random _random;
        private float _createMeteorCountdown;
        Projectiles _projectiles;
        public void Initialize()
        {
            _random = new Random();
            _createMeteorCountdown = 10f;

            _projectiles = new Projectiles();

            _input = new InputHandler();
            _collision = new CollisionHandler();
            _collisionDebug = new CollisionDebug();
            
            _gameObjects = new Dictionary<string,List<GameObject>>();
            
            //Alitsa
            _alitsa = new SpaceShip("Alitsa",new Rectangle(120,604,104,84),
                                    		 new Vector2(128,84/2));

            var _alitsaChildrens = new List<GameObject>();
            _alitsaChildrens.Add(_alitsa);

            _input.Bind(Keys.W,new MoveUpCommand(_alitsa));
            _input.Bind(Keys.S,new MoveDownCommand(_alitsa));
            _input.Bind(Keys.LeftShift,new ShootCommand(_alitsa));

            _gameObjects.Add(_alitsa.Name,_alitsaChildrens);

            //Atsin
            _atsin = new SpaceShip("Atsin",new Rectangle(518,493,82,84),
                                   		   new Vector2(128,Global.ScreenHeight-(84/2)));

             var _atsinChildrens = new List<GameObject>();
            _atsinChildrens.Add(_atsin);

            _input.Bind(Keys.Up,new MoveUpCommand(_atsin));
            _input.Bind(Keys.Down,new MoveDownCommand(_atsin));
            _input.Bind(Keys.RightShift,new ShootCommand(_atsin));

            _gameObjects.Add(_atsin.Name,_atsinChildrens);

            //_collision.WhenCollide("Alitsa","Atsin",new IHealthAction("Atsin",-1));

            var MeteorAndAlitsaCollideActions = new List<ICollisionAction>();
            MeteorAndAlitsaCollideActions.Add(new HealthAction(_alitsa.Name,-1));
            MeteorAndAlitsaCollideActions.Add(new HealthAction("Meteor",-100));
            _collision.WhenCollide("Meteor",_alitsa.Name,MeteorAndAlitsaCollideActions);

            var MeteorAndAtsinCollideActions = new List<ICollisionAction>();
            MeteorAndAtsinCollideActions.Add(new HealthAction(_atsin.Name,-1));
            MeteorAndAtsinCollideActions.Add(new HealthAction("Meteor",-100));
            _collision.WhenCollide("Meteor",_atsin.Name,MeteorAndAtsinCollideActions);
            
            _gameObjects.Add("Meteor",new List<GameObject>());

            var alitsaProjectileAndMeteorCollideActions = new List<ICollisionAction>();
            alitsaProjectileAndMeteorCollideActions.Add(new HealthAction("Projectile"+_alitsa.Name,-100));
            alitsaProjectileAndMeteorCollideActions.Add(new HealthAction("Meteor",-5));
            _collision.WhenCollide("Projectile"+_alitsa.Name,"Meteor",alitsaProjectileAndMeteorCollideActions);

            var atsinProjectileAndMeteorCollideActions = new List<ICollisionAction>();
            atsinProjectileAndMeteorCollideActions.Add(new HealthAction("Projectile"+_atsin.Name,-100));
            atsinProjectileAndMeteorCollideActions.Add(new HealthAction("Meteor",-5));
            atsinProjectileAndMeteorCollideActions.Add(new HealthAction(_atsin,+1));
            _collision.WhenCollide("Projectile"+_atsin.Name,"Meteor",atsinProjectileAndMeteorCollideActions);
            
            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
                	gameObject.Initialize();
                }
            }

            _backgroundPosition = Vector2.Zero;
            _backgroundScrollSpeed = 50.0f;
        }
        public void LoadContent(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>("Background");
        }
        public void Update(GameTime gameTime)
        {
            _input.HandleInput();
            _projectiles.Update(_gameObjects);

            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _backgroundPosition.X -= _backgroundScrollSpeed * elapsedTime;
            if(_backgroundPosition.X <= -_backgroundTexture.Width)
                _backgroundPosition.X = 0.0f;

            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
            		if(gameObject.IsActive) gameObject.Update(gameTime);
            	}
            }
            
            _collision.HandleCollision(_gameObjects);
            _createMeteorCountdown -= elapsedTime;
            if(_createMeteorCountdown <= 0.0)
            {
                Rectangle meteorVariation = _random.Next(0,1) == 0 ? new Rectangle(346,814,18,18) :
                                                                     new Rectangle(399,814,16,15);
                var meteorRandomPosition =
                    new Vector2(_random.Next(((int)Global.ScreenWidth-20)/2,(int)Global.ScreenWidth-20),
                                _random.Next(128,(int)Global.ScreenHeight-128));
                 _gameObjects["Meteor"].Add(new Meteor("Meteor",meteorVariation,meteorRandomPosition,75f));
                _createMeteorCountdown = 0.8f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Background
            for(int y = (int)_backgroundPosition.Y;y < Global.ScreenHeight;y += _backgroundTexture.Height)
            {
                for(int x = (int)_backgroundPosition.X;x < Global.ScreenWidth;x += _backgroundTexture.Width)
                {
					spriteBatch.Draw(_backgroundTexture,new Vector2(x,y),Color.White);
                }
            }
            //Draw objects
            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
                    if(gameObject.IsActive)
                    {
                        gameObject.Draw(spriteBatch);
                        _collisionDebug.Draw(spriteBatch,gameObject);
                    }
                }
            }
        }
    }
}
