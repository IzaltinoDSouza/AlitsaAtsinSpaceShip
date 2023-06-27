using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class Universe
    {
        private BackgroundScrollable _background;
        private CollisionHandler _collison;
        private List<MeteorWave> _meteorWaves;
        private Dictionary<string,List<GameObject>> _gameObjects;
        private Projectiles _projectiles;

        public Universe(BackgroundScrollable background,CollisionHandler collision,List<MeteorWave> meteorWaves)
        {
            _background = background;
            _collison = collision;
            _meteorWaves = meteorWaves;
            _gameObjects = new Dictionary<string,List<GameObject>>();
            _projectiles = new Projectiles();
        }
        public void AddGameObject(string name,GameObject gameobject)
        {
            List<GameObject> tmp = new List<GameObject>();
            gameobject.Initialize();
            tmp.Add(gameobject);
            _gameObjects.Add(name,tmp);
        }
        public void AddGameObject(string name,List<GameObject> gameobject)
        {
            foreach(var gameObject in gameobject)
            {
                gameObject.Initialize();
            }
            _gameObjects.Add(name,gameobject);
        }
        public void AddGameObjectChild(string parentName, GameObject gameobject)
        {
            if(_gameObjects.ContainsKey(parentName))
            {
                _gameObjects[parentName].Add(gameobject);
            }
        }
        public bool Update(GameTime gameTime)
        {
            _background.Update(gameTime);
            _collison.HandleCollision(_gameObjects);
            foreach(var meteorWave in _meteorWaves)
            {
               meteorWave.Update(gameTime,_gameObjects);
            }
            _projectiles.Update(_gameObjects);
            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
                    if(gameObject.IsActive) gameObject.Update(gameTime);
                    /*if(gameObject is SpaceShip spaceship)
                    {
                        System.Console.WriteLine($"{spaceship.Name} : Health : {spaceship.CurrentHealth} | Score : {spaceship.Score} | Shield Total : {spaceship.ShieldCount} | Shield TimeLeft : {spaceship.ShieldDuration}");
                    }*/
                }
            }
            return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            foreach(var gameObjects in _gameObjects)
            {
            	foreach(var gameObject in gameObjects.Value)
            	{
                    if(gameObject.IsActive)
                    {
                        gameObject.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}