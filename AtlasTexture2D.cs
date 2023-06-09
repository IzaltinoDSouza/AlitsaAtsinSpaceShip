using Microsoft.Xna.Framework;

namespace AASS
{
    namespace AtlasTexture
    {
        public class AtlasTexture2D
        {
            public int AtlasID {get; set; }
            public Rectangle AtlasPosition {get; set; }

            public AtlasTexture2D(int atlasID,Vector2 position,Vector2 size)
            {
                AtlasID = atlasID;
                AtlasPosition = new Rectangle((int)position.X,(int)position.Y,
                                     (int)size.X,(int)size.Y);
            }
            public AtlasTexture2D(int atlasID,Rectangle atlasPosition)
            {
                AtlasID = atlasID;
                AtlasPosition = atlasPosition;
            }
        }
    }
}