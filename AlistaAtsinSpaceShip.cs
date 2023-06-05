using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AASS.AtlasTexture;

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
                                   new AtlasTexture2D(0,new Vector2(120,604),
                                                        new Vector2(104,84)),
                                                        position);
	    _spriteBatch.End();
        base.Draw(gameTime);
    }
}
