using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
public class AlistaAtsinSpaceShip : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Vector2 position;
    
    public AlistaAtsinSpaceShip()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        position = new Vector2(104/2,84/2);
        
        AtlasTexture2DManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AtlasTexture2DManager.AddTexture2D(Content.Load<Texture2D>("AtlasTextures/GameAtlas"));
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();
        AtlasTexture2DManager.Draw(_spriteBatch,
                                   new AtlasTexture2D(1,new Vector2(120,604),
                                                        new Vector2(104,84)),
                                                        position);
	    _spriteBatch.End();
        base.Draw(gameTime);
    }
}
