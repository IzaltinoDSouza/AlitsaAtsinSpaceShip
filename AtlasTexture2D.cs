using Microsoft.Xna.Framework;

namespace AASS
{
    namespace AtlasTexture
    {
        public class AtlasTexture2D
        {
            public int AtlasID {get; set; }
            public Vector2 Position {get; set; }
            public Vector2 Size {get; set; }

            public AtlasTexture2D(int atlasID,Vector2 position,Vector2 size)
            {
                AtlasID = atlasID;
                Position = position;
                Size = size;
            }
        }
    }
}