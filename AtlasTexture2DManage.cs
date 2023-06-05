using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using AASS.AtlasTexture;

namespace AASS
{
    namespace AtlasTexture
    {
        static public class AtlasTexture2DManager
        {
        static private List<Texture2D> _atlas_textures;

            static public void Initialize()
            {
                _atlas_textures = new List<Texture2D>();
            }
            static public int AddTexture2D(Texture2D texture)
            {
                _atlas_textures.Add(texture);
                return _atlas_textures.Count-1;
            }
            static public void Draw(SpriteBatch spriteBatch,
                                    AtlasTexture2D atlasTexture2D,
                                    Vector2 position)
            {    
                if(atlasTexture2D.AtlasID < _atlas_textures.Count)
                {
                    spriteBatch.Draw(_atlas_textures[atlasTexture2D.AtlasID],
                                    new Rectangle((int)position.X,(int)position.Y,
                                                    (int)atlasTexture2D.Size.X,(int)atlasTexture2D.Size.Y),
                                    new Rectangle((int)atlasTexture2D.Position.X,(int)atlasTexture2D.Position.Y,
                                                (int)atlasTexture2D.Size.X,(int)atlasTexture2D.Size.Y),
                                    Color.White,0.0f,
                                    new Vector2(atlasTexture2D.Size.X/2,atlasTexture2D.Size.Y/2),
                                    SpriteEffects.None,0);
                }
                else
                {
                    //draw a pink rectangle to show something is wrong
                    Console.WriteLine("TODO : Atlas ID is invalid");
                    Environment.Exit(0);
                }
            }
        }
    }
}