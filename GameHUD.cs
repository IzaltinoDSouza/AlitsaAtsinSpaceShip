using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AASS.AtlasTexture;

namespace AASS
{
    class SpaceshipHUD
    {
        private Rectangle _layout;
        private SpaceShip _spaceship;
        private Rectangle _atlasShield;
        private Rectangle _atlasShieldEmpty;
        public SpaceshipHUD(SpaceShip spaceship,Rectangle layout)
        {
            _spaceship = spaceship;
            _layout = layout;
           _atlasShield = new Rectangle(776,928,34,33);
           _atlasShieldEmpty = new Rectangle(491,182,34,33);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0;i < _spaceship.ShieldMaxCount;++i)
            {
            	var position = new Vector2(_layout.X + (i*_atlasShieldEmpty.Width),_layout.Y + ((_layout.Height-_atlasShieldEmpty.Height)/2));
            	AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasShieldEmpty),
                                                            position);
            }
            for(int i = 0;i < _spaceship.ShieldCount;++i)
            {
            	var position = new Vector2(_layout.X + (i*_atlasShield.Width),_layout.Y + ((_layout.Height-_atlasShield.Height)/2));
            	AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_atlasShield),
                                                            position);
            }

            //Score
            var scoreStr = _spaceship.Score.ToString();
            var fontSize = Global.DefaultFont.MeasureString(scoreStr);
            spriteBatch.DrawString(Global.DefaultFont,scoreStr,new Vector2(_layout.X + (3*_atlasShield.Width),_layout.Y + ((_layout.Height-_atlasShield.Height-fontSize.Y)/2)),Color.White);
        }
    }
    class GameHUD
    {
        private SpaceshipHUD _alitsa;
        private SpaceshipHUD _atsin;
        public GameHUD(SpaceShip alitsa,SpaceShip atsin)
        {
            _alitsa = new SpaceshipHUD(alitsa,new Rectangle(34,20,200,35));
            _atsin  = new SpaceshipHUD(atsin,new Rectangle(34,55,200,35));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _alitsa.Draw(spriteBatch);
            _atsin.Draw(spriteBatch);
        }
    }
}
