using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using AASS;
using AASS.AtlasTexture;

public class AlistaAtsinSpaceShip : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Universe _universe;

    public AlistaAtsinSpaceShip()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        Global.SetScreenSize((float)_graphics.PreferredBackBufferWidth,
                             (float)_graphics.PreferredBackBufferHeight);

        AtlasTexture2DManager.Initialize();

        _universe = new Universe();

        _universe.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Global.SetScreenSize(Content.Load<Texture2D>("TextureDebug"));
        Global.SetDefaultFont(Content.Load<SpriteFont>("Fonts/DefaultNormal14"));
        AtlasTexture2DManager.AddTexture2D(Content.Load<Texture2D>("AtlasTextures/GameAtlas"));
        _universe.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _universe.Update(gameTime);

        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _universe.Draw(_spriteBatch);
        
	    _spriteBatch.End();
        base.Draw(gameTime);
    }
}
