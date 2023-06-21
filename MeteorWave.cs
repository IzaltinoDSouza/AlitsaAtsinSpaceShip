using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AASS
{
    class MeteorWave
    {
        private string _meteorName;
        private List<Rectangle> _meteorVariations;
        private int _minMeteorPerWave;
        private int _maxMeteorPerWave;
        private float _meteorSpeed;
        private float _waveTime;
        private float _nextWaveTime;
        private Random _random;
        public MeteorWave(string meteorName,List<Rectangle> meteorVariations,int minMeteorsPerWave,int maxMeteorsPerWave,float meteorSpeed,float waveTime)
        {
            _meteorName = meteorName;
            _meteorVariations = meteorVariations;
            _minMeteorPerWave = minMeteorsPerWave;
            _maxMeteorPerWave = maxMeteorsPerWave;
            _meteorSpeed = meteorSpeed;
            _waveTime = waveTime;
            _nextWaveTime = 0;
            _random = new Random();
        }

        public void Update(GameTime gameTime, Dictionary<string,List<GameObject>> _gameObjects)
        {
            _nextWaveTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(_nextWaveTime >= _waveTime)
            {
                var meteorPerWave = _random.Next(_minMeteorPerWave,_maxMeteorPerWave);
                for(int i = 0;i < meteorPerWave;++i)
                {
                    var meteorVariation = _meteorVariations[_random.Next(0,_meteorVariations.Count-1)];
                    
                        Vector2 meteorPosition = new Vector2(_random.Next(((int)Global.ScreenWidth-20)/2,(int)Global.ScreenWidth-20),
                                                             _random.Next(135,(int)Global.ScreenHeight-120));
                        
                        _gameObjects[_meteorName].Add(new Meteor(_meteorName,meteorVariation,meteorPosition,_meteorSpeed));
                        
                }
                _nextWaveTime = 0;
            }
        }
    }
}