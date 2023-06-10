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
                                   new Vector2(104/2,84/2));

            _input.Bind(Keys.W,new MoveUpCommand(_alitsa));
            _input.Bind(Keys.S,new MoveDownCommand(_alitsa));
            
            _gameObjects.Add(_alitsa);
            
            //Atsin
            _atsin = new SpaceShip(new Rectangle(518,493,82,84),
                                   new Vector2(104/2,360));
            
            _input.Bind(Keys.Up,new MoveUpCommand(_atsin));
            _input.Bind(Keys.Down,new MoveDownCommand(_atsin));
            
            _gameObjects.Add(_atsin);
            
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Initialize();
            }
        }
        public void LoadContent(ContentManager content)
        {

        }
        public void Update(GameTime gameTime)
        {
            _input.HandleInput();

            foreach(var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            foreach(var gameObject in _gameObjects)
            {
                gameObject.Draw(sprite);
            }
        }
    }
}