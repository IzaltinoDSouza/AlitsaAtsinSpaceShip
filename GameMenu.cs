using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    enum GameMenuState
    {
        GameMenu = -1,
        NewGame = 0,
        //LoadGame = 1,
        Quit = 1
    }
    class GameMenu
    {
        private BackgroundScrollable _background;
        private List<Menu> _menus;
        private int _prevMouseHover;
        public GameMenu()
        {
            _background = new BackgroundScrollable(Vector2.Zero,0.0f);
            _menus = new List<Menu>();
            _prevMouseHover = -1;

            _menus.Add(new Menu("New Game",Vector2.Zero));
            //_menus.Add(new Menu("Load Game",Vector2.Zero));
            _menus.Add(new Menu("Quit",Vector2.Zero));

            var menuPosition = new Vector2((Global.ScreenWidth - _menus[0].GetWidth())/2,
                                           _menus[0].GetHeight() * _menus.Count);
            for(int i = 0;i < _menus.Count;++i)
            {
                _menus[i].SetPosition(new Vector2(menuPosition.X,
                                                  menuPosition.Y + i * (_menus[i].GetHeight()*2)));
            }
        }
        public void LoadContent(ContentManager content)
        {
            _background.SetBackground(content.Load<Texture2D>("Background/DarkPurple"));
        }
        public GameMenuState Update(GameTime gametime)
        {
            bool isMouseHover = false;
            for(int i = 0;i < _menus.Count;++i)
            {
                _menus[i].Update(gametime);
                if(_menus[i].IsClick())
                {
                    return (GameMenuState)i;
                }
                if(_menus[i].IsMouseHover())
                {
                    if(_prevMouseHover != i)
                    {
                        Global.SFXSounds["MenuMouseHover"].Stop();
                        Global.SFXSounds["MenuMouseHover"].Play();
                        _prevMouseHover = i;
                    }
                    isMouseHover = true;
                }
            }
            //This reset it, because there was not a mouse hover.
            if(!isMouseHover)
            {
                _prevMouseHover = -1;
            }
            return GameMenuState.GameMenu;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _background.Draw(spriteBatch);
            for(int i = 0;i < _menus.Count;++i)
            {
                _menus[i].Draw(spriteBatch);
            }
        }
    }
}