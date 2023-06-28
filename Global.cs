using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace AASS
{
    static public class Global
    {
        static public float ScreenWidth{get;private set;}
        static public float ScreenHeight{get;private set;}
        static public Texture2D TextureDebug {get;private set;}
        static public SpriteFont DefaultFont {get;private set;}
        static public Dictionary<string,SoundEffectInstance> SFXSounds{get;private set;}
        static public void SetScreenSize(float width,float height)
        {
            ScreenWidth  = width;
            ScreenHeight = height;
        }
        static public void SetScreenSize(Texture2D textureDebug)
        {
            TextureDebug = textureDebug;
        }
        static public void SetDefaultFont(SpriteFont font)
        {
            DefaultFont = font;
        }
        static public void AddSFXSound(string name,SoundEffect sfxSound)
        {
            if(SFXSounds == null) SFXSounds = new Dictionary<string,SoundEffectInstance>();
            SFXSounds.Add(name,sfxSound.CreateInstance());
        }
    }
}