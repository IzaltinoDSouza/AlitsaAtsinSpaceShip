using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AASS
{
    class Menu
    {
        private string _text;
        private Color _textColor;
        private Vector2 _position;
        private Rectangle _buttonAtlas;
        private Vector2 _buttonAtlasCenter;
        private bool _isClick;
        private bool _isMouseHover;
        public Menu(string text,Vector2 position)
        {
            _text = text;
            _position = position;
            _buttonAtlas = new Rectangle(0,117,222,39);
            _buttonAtlasCenter = new Vector2(_buttonAtlas.Width/2,
                                             _buttonAtlas.Height/2);
        }
        public void Update(GameTime gameTime)
        {
            _isMouseHover = false;
            _isClick = false;
            _textColor = Color.Black;
            MouseState mState = Mouse.GetState();
            if(IsCursorInsideBounds(mState.Position))
            {
                _isMouseHover = true;
                _textColor = Color.DarkRed;
            }
            if(ButtonState.Pressed == mState.LeftButton && _isMouseHover)
            {
                _isClick = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                                    new AtlasTexture2D(0,_buttonAtlas),
                                                    _position+_buttonAtlasCenter);
            var fontSizeCenter = Global.DefaultFont.MeasureString(_text)/2;
            var textPosition  = _position + (_buttonAtlasCenter-fontSizeCenter);
            spriteBatch.DrawString(Global.DefaultFont,_text,textPosition,_textColor);
        }
        public void SetPosition(Vector2 position)
        {
            _position = position;
        }
        public void SetPosition(float x,float y)
        {
            _position.X = x;
            _position.Y = y;
        }
        public Vector2 GetPosition()
        {
            return _position;
        }
        public float GetWidth()
        {
            return _buttonAtlas.Width;
        }
        public float GetHeight()
        {
            return _buttonAtlas.Height;
        }
        public bool IsClick()
        {
            return _isClick;
        }
        public bool IsMouseHover()
        {
           return _isMouseHover;
        }
        private bool IsCursorInsideBounds(Point cursorPosition)
        {
            return _position.X  <= cursorPosition.X && _position.X + _buttonAtlas.Width  >= cursorPosition.X &&
                   _position.Y  <= cursorPosition.Y && _position.Y + _buttonAtlas.Height >= cursorPosition.Y;
        }
    }
}