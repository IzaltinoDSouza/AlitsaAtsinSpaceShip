using System.Collections.Generic;
using AASS.AtlasTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AASS
{
    class SpaceShipChooser
    {
        private List<List<Rectangle>> _spaceships;
        private const int TOTAL_SPACESHIP_MODELS = 5;
        private const int TOTAL_SPACESHIP_COLORS = 4;
        private float _angle;
        private int selectedSpaceshipModel;
        private int selectedSpaceshipColor;
        private float countdownTimer;
        public SpaceShipChooser()
        {
            _spaceships = new List<List<Rectangle>>();

            var purpleModels = new List<Rectangle>();
            purpleModels.Add(new Rectangle(423,728,93,84));
            purpleModels.Add(new Rectangle(120,604,104,84));
            purpleModels.Add(new Rectangle(144,156,103,84));
            purpleModels.Add(new Rectangle(518,325,82,84));
            purpleModels.Add(new Rectangle(346,150,97,84));

            var blueModels = new List<Rectangle>();
            blueModels.Add(new Rectangle(425,468,93,84));
            blueModels.Add(new Rectangle(143,293,104,84));
            blueModels.Add(new Rectangle(222,0,103,84));
            blueModels.Add(new Rectangle(518,409,82,84));
            blueModels.Add(new Rectangle(421,814,97,84));

            var greenModels = new List<Rectangle>();
            greenModels.Add(new Rectangle(425,552,93,84));
            greenModels.Add(new Rectangle(133,412,104,84));
            greenModels.Add(new Rectangle(224,496,103,84));
            greenModels.Add(new Rectangle(518,493,82,84));
            greenModels.Add(new Rectangle(408,907,97,84));

            var orangeModels = new List<Rectangle>();
            orangeModels.Add(new Rectangle(425,384,93,84));
            orangeModels.Add(new Rectangle(120,520,104,84));
            orangeModels.Add(new Rectangle(224,580,103,84));
            orangeModels.Add(new Rectangle(520,577,82,84));
            orangeModels.Add(new Rectangle(423,644,97,84));

            _spaceships.Add(purpleModels);
            _spaceships.Add(blueModels);
            _spaceships.Add(greenModels);
            _spaceships.Add(orangeModels);

            _angle = MathHelper.ToRadians(90);

            selectedSpaceshipColor = 0;
            selectedSpaceshipModel = 0;

            countdownTimer = 0.0f;
        }
        public void Update(GameTime gameTime)
        {
            float direction = 0;
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = -1.0f;
            }else if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = +1.0f;
            }

            if(countdownTimer <= 0.0f && direction != 0)
            {
                if(direction < 0)
                {
                    selectedSpaceshipModel -= 1;
                    if(selectedSpaceshipModel < 0)
                        selectedSpaceshipModel = 0;
                }
                if(direction > 0)
                {
                    selectedSpaceshipModel += 1;
                    if(selectedSpaceshipModel > TOTAL_SPACESHIP_MODELS-1)
                        selectedSpaceshipModel = TOTAL_SPACESHIP_MODELS-1;

                }
                countdownTimer = 0.5f;
            }
            countdownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            AtlasTexture.AtlasTexture2DManager.Draw(spriteBatch,
                                    new AtlasTexture2D(0,_spaceships[selectedSpaceshipColor][selectedSpaceshipModel]),
                                                            new Vector2((Global.ScreenWidth - _spaceships[selectedSpaceshipColor][selectedSpaceshipModel].Width)/2,(Global.ScreenHeight - _spaceships[selectedSpaceshipColor][selectedSpaceshipModel].Height)/2),_angle,
                                                            SpriteEffects.FlipVertically);
        }
    }
}