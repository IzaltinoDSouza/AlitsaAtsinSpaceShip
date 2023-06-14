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

        public void Initialize()
        {
            _input = new InputHandler();
            _collision = new CollisionHandler();
            
            _gameObjects = new Dictionary<string,List<GameObject>>();
            
            //Alitsa
            _alitsa = new SpaceShip("Alitsa",new Rectangle(120,604,104,84),
                                    		 new Vector2(128,84/2));
            
            var _alitsaChildrens = new List<GameObject>();
            _alitsaChildrens.Add(_alitsa);

            _input.Bind(Keys.W,new MoveUpCommand(_alitsa));
            _input.Bind(Keys.S,new MoveDownCommand(_alitsa));

            _gameObjects.Add(_alitsa.Name,_alitsaChildrens);
            
            //Atsin
            _atsin = new SpaceShip("Atsin",new Rectangle(518,493,82,84),
                                   		   new Vector2(128,Global.ScreenHeight-(84/2)));
            
             var _atsinChildrens = new List<GameObject>();
            _atsinChildrens.Add(_atsin);

            _input.Bind(Keys.Up,new MoveUpCommand(_atsin));
            _input.Bind(Keys.Down,new MoveDownCommand(_atsin));
            
            _gameObjects.Add(_atsin.Name,_atsinChildrens);
            
            _collision.WhenCollide("Alitsa","Atsin",new IHealthAction("Atsin",-1));
            
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
        }
        public void Draw(SpriteBatch sprite)
        {
            //Background
            for(int y = (int)_backgroundPosition.Y;y < Global.ScreenHeight;y += _backgroundTexture.Height)
            {
                for(int x = (int)_backgroundPosition.X;x < Global.ScreenWidth;x += _backgroundTexture.Width)
                {
                    sprite.Draw(_backgroundTexture,new Vector2(x,y),Color.White);
                }
            }
            //Draw objects
            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
                	if(gameObject.IsActive) gameObject.Draw(sprite);
                }
            }
        }
    }
}
