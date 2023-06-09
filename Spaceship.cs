using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    class SpaceShip : GameObject
    {
        private Rectangle _atlasSpaceship;
        public SpaceShip(Rectangle atlasSpaceship)
        {
            _atlasSpaceship = atlasSpaceship;
            Position = Vector2.Zero;
            IsActive = true;
        }
        public SpaceShip(Rectangle atlasSpaceship,Vector2 position)
        {
            _atlasSpaceship = atlasSpaceship;
            Position = position;
            IsActive = true;
        }
        public override void Initialize()
        {
            //TODO
        }
        public override void Update(GameTime gameTime)
        {
            //TODO
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasSpaceship),
                                                            Position);
        }

    }
}