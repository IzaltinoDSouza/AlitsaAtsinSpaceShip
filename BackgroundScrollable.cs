using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class BackgroundScrollable
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;
        public BackgroundScrollable(Vector2 position,float speed)
        {
            _position = position;
            _speed = speed;
        }
        public void SetBackground(Texture2D background)
        {
            _texture = background;
        }
        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _position.X -= _speed * elapsedTime;
            if(_position.X <= -_texture.Width)
                _position.X = 0.0f;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int y = (int)_position.Y;y < Global.ScreenHeight;y += _texture.Height)
            {
                for(int x = (int)_position.X;x < Global.ScreenWidth;x += _texture.Width)
                {
					spriteBatch.Draw(_texture,new Vector2(x,y),Color.White);
                }
            }
        }
    }
}