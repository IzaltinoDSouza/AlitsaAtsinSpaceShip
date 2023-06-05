using Microsoft.Xna.Framework;

namespace AASS
{
    namespace AtlasTexture
    {
        public class AtlasTexture2D
        {
            public int AtlasID {get; set; }
            public Rectangle Rect {get; set; }

            public AtlasTexture2D(int atlasID,Vector2 position,Vector2 size)
            {
                AtlasID = atlasID;
                Rect = new Rectangle((int)position.X,(int)position.Y,
                                     (int)size.X,(int)size.Y);
            }
        }
    }
}