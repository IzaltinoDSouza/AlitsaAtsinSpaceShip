using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    abstract class Level
    {
        abstract public void Initialize(int level,SpaceShip alitsa,SpaceShip atsin);
        abstract public void LoadContent(ContentManager content);
        abstract public bool Update(GameTime gameTime);
        abstract public void Draw(SpriteBatch spriteBatch);
    }
}