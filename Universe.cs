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
        private List<GameObject> _gameObjects;
        private SpaceShip _alitsa;
        private SpaceShip _atsin;
        private InputHandler _input;

        public void Initialize()
        {
            _input = new InputHandler();
            
            _gameObjects = new List<GameObject>();
            
            //Alitsa
            _alitsa = new SpaceShip(new Rectangle(120,604,104,84),
                                    new Vector2(128,84/2));

            _input.Bind(Keys.W,new MoveUpCommand(_alitsa));
            _input.Bind(Keys.S,new MoveDownCommand(_alitsa));
            
            _gameObjects.Add(_alitsa);
            
            //Atsin
            _atsin = new SpaceShip(new Rectangle(518,493,82,84),
                                   new Vector2(128,Global.ScreenHeight-(84/2)));
            
            _input.Bind(Keys.Up,new MoveUpCommand(_atsin));
            _input.Bind(Keys.Down,new MoveDownCommand(_atsin));
            
            _gameObjects.Add(_atsin);
            
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Initialize();
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

            foreach(var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }
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
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Draw(sprite);
            }
        }
    }
}