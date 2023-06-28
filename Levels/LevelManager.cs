using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    enum LevelManagerState
    {
        Loading,
        InitializeCurrentLevel,
        PlayCurrentLevel,
        LoadNextLevel
    }
    class LevelManager
    {
        private LevelManagerState _state;
        private List<Level> _levels;
        private int _currentLevel;
        private ContentManager _content;
        private SpaceShip _alitsa;
        private SpaceShip _atsin;
        private float _loadingTime;
        public LevelManager(int level,SpaceShip alitsa,SpaceShip atsin)
        {
            _levels = new List<Level>();
            _levels.Add(new Level01());
            _levels.Add(new Level02());
            _levels.Add(new Level03());
            _levels.Add(new Level04());
            _levels.Add(new Level05());
            _levels.Add(new Level06());
            _levels.Add(new Level07());
            _levels.Add(new Level08());
            _levels.Add(new Level09());
            _levels.Add(new Level10());
            _currentLevel = level-1;
            _state = LevelManagerState.Loading;

            _alitsa = alitsa;
            _atsin = atsin;
            _loadingTime = 5f;
        }
        public void LoadContent(ContentManager content)
        {
            _content = content;
        }
        public bool Update(GameTime gameTime,InputHandler input)
        {
            switch(_state)
            {
                case LevelManagerState.Loading:
                    _loadingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if(_loadingTime <= 0)
                    {
                       _state = LevelManagerState.InitializeCurrentLevel;
                    }
                break;
                case LevelManagerState.InitializeCurrentLevel:
                    _alitsa.Position = new Vector2(128,84+50);
                    _atsin.Position = new Vector2(128,Global.ScreenHeight-84);
                    _levels[_currentLevel].Initialize(_currentLevel,_alitsa,_atsin);
                    _levels[_currentLevel].LoadContent(_content);
                    _state = LevelManagerState.PlayCurrentLevel;
                break;
                case LevelManagerState.PlayCurrentLevel:
                    if(_levels[_currentLevel].Update(gameTime))
                    {
                        input.HandleInput();
                    }else
                    {
                        _state = LevelManagerState.LoadNextLevel;
                    }
                break;
                case LevelManagerState.LoadNextLevel:
                    _currentLevel += 1;
                    if(_currentLevel < _levels.Count)
                    {
                        _state = LevelManagerState.Loading;
                        _loadingTime = 3.0f;
                    }else
                    {
                        return false;
                    }
                break;
            }
            return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch(_state)
            {
                case LevelManagerState.Loading:
                    string textLoading = "Loading";
                    var textLoadingSize = Global.DefaultFont.MeasureString(textLoading);
                    spriteBatch.DrawString(Global.DefaultFont,textLoading,
                                            new Vector2((Global.ScreenWidth-textLoadingSize.X)/2,
                                                        (Global.ScreenHeight-textLoadingSize.Y)/2),
                                            Color.White);
                break;
                case LevelManagerState.PlayCurrentLevel:
                    _levels[_currentLevel].Draw(spriteBatch);
                break;
            }
        }
        public void SetLevel(int level = 1)
        {
            _currentLevel = level;
        }
    }
}