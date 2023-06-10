namespace AASS
{
    static public class Global
    {
        static public float ScreenWidth{get;private set;}
        static public float ScreenHeight{get;private set;}

        static public void SetScreenSize(float width,float height)
        {
            ScreenWidth  = width;
            ScreenHeight = height;
        }
    }
}