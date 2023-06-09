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

        public void Initialize()
        {
            _gameObjects = new List<GameObject>();

           _alitsa = new SpaceShip(new Rectangle(120,604,104,84),
                                   new Vector2(104/2,84/2));

            _gameObjects.Add(_alitsa);

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
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _alitsa.MoveUp();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _alitsa.MoveDown();
            }

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