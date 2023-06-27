using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AASS
{
    class ShieldPowerUpWave
    {
        private float _speed;
        private float _waveTime;
        private float _nextWaveTime;
        private Random _random;
        public ShieldPowerUpWave(float speed,float waveTime)
        {
            _speed = speed;
            _waveTime = waveTime;
            _nextWaveTime = 0;
            _random = new Random();
        }
        public void Update(GameTime gameTime, Dictionary<string,List<GameObject>> _gameObjects)
        {
            _nextWaveTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_nextWaveTime >= _waveTime)
            {    
                Vector2 randomPosition = new Vector2(_random.Next(((int)Global.ScreenWidth-35)/2,(int)Global.ScreenWidth-35),
                                                     _random.Next(135,(int)Global.ScreenHeight-120));
                _gameObjects["ShieldPowerUp"].Add(new ShieldPowerUp(randomPosition,_speed));
                _nextWaveTime = 0;
            }
        }
    }
}