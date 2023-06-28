namespace AASS
{
    class PlaySFXSoundAction : ICollisionAction
    {
        string _sfxSoundName;
        public PlaySFXSoundAction(string sfxSoundName)
        {
            _sfxSoundName = sfxSoundName;
        }
        public void Execute(GameObject obj1, GameObject obj)
        {
            Global.SFXSounds[_sfxSoundName].Stop();
            Global.SFXSounds[_sfxSoundName].Play();
        }
    }
}