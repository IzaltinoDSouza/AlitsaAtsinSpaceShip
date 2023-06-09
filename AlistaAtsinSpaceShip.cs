using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using AASS;
using AASS.AtlasTexture;
using Microsoft.Xna.Framework.Content;

class SpaceShip : GameObject
{
    private Rectangle _atlasSpaceship;
    public SpaceShip(Rectangle atlasSpaceship)
    {
        _atlasSpaceship = atlasSpaceship;
        Position = Vector2.Zero;
        IsActive = true;
    }
    public SpaceShip(Rectangle atlasSpaceship,Vector2 position)
    {
        _atlasSpaceship = atlasSpaceship;
        Position = position;
        IsActive = true;
    }
    public override void Initialize()
    {
        //TODO
    }
    public override void Update(GameTime gameTime)
    {
        //TODO
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        AtlasTexture2DManager.Draw(spriteBatch,
                                   new AtlasTexture2D(0,_atlasSpaceship),
                                                        Position);
    }
    
}
public class AlistaAtsinSpaceShip : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private SpaceShip _alitsa;

    public AlistaAtsinSpaceShip()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {        
        AtlasTexture2DManager.Initialize();

        _alitsa = new SpaceShip(new Rectangle(120,604,104,84),
                                new Vector2(104/2,84/2));
        _alitsa.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AtlasTexture2DManager.AddTexture2D(Content.Load<Texture2D>("AtlasTextures/GameAtlas"));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _alitsa.Update(gameTime);

        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        _alitsa.Draw(_spriteBatch);
        
	    _spriteBatch.End();
        base.Draw(gameTime);
    }
}
