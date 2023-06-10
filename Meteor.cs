using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using AASS.AtlasTexture;

namespace AASS
{
    public class Meteor : GameObject
    {
        private Rectangle _atlasMeteor;
        private float _speed;

        public Meteor(Rectangle atlasMeteor,
                      Vector2 position,
                      float speed)
        {
            _atlasMeteor = atlasMeteor;
            Position = position;
            _speed = speed;
        }
        public override void Initialize()
        {
           //Nothing
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X -= _speed * elapsedTime;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasMeteor),
                                                       Position);
        
        }
    }
}