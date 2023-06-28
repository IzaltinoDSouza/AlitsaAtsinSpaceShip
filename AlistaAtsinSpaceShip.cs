using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using AASS;
using AASS.AtlasTexture;

enum GameState
{
    GameMenu,
    PlayGame,
    EndOfAllLevels,
    GameOver,
    Credits
}
public class AlistaAtsinSpaceShip : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private InputHandler _input;
    private GameState _gameState;
    private GameMenu _gameMenu;
    private LevelManager _levelManager;
    private GameHUD _gameHUD;

    private SpaceShip _alitsa;
    private SpaceShip _atsin;

    private float _countdown;

    private BackgroundScrollable _background;
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

        _input = new InputHandler();
        _gameState = GameState.GameMenu;
        _gameMenu = new GameMenu();

        _alitsa = new SpaceShip("Alitsa",new Rectangle(120,604,104,84));
        _input.Bind(Keys.W,new MoveUpCommand(_alitsa));
        _input.Bind(Keys.S,new MoveDownCommand(_alitsa));
        _input.Bind(Keys.LeftShift,new ShootCommand(_alitsa));
        _input.Bind(Keys.LeftControl,new ShieldActivateCommand(_alitsa));

        _atsin = new SpaceShip("Atsin",new Rectangle(518,493,82,84));
        _input.Bind(Keys.Up,new MoveUpCommand(_atsin));
        _input.Bind(Keys.Down,new MoveDownCommand(_atsin));
        _input.Bind(Keys.RightShift,new ShootCommand(_atsin));
        _input.Bind(Keys.RightControl,new ShieldActivateCommand(_atsin));

        _levelManager = new LevelManager(1,_alitsa,_atsin);
        _gameHUD = new GameHUD(_alitsa,_atsin);

        _countdown = 10.0f;

        _background = new BackgroundScrollable(Vector2.Zero,0.0f);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Global.SetScreenSize(Content.Load<Texture2D>("TextureDebug"));
        Global.SetDefaultFont(Content.Load<SpriteFont>("Fonts/DefaultNormal14"));
        AtlasTexture2DManager.AddTexture2D(Content.Load<Texture2D>("AtlasTextures/GameAtlas"));
        Mouse.SetCursor(MouseCursor.FromTexture2D(Content.Load<Texture2D>("Cursor"),0,0));
        _gameMenu.LoadContent(Content);
        _levelManager.LoadContent(Content);
        _background.SetBackground(Content.Load<Texture2D>("Background/DarkPurple"));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch(_gameState)
        {
            case GameState.GameMenu:
                IsMouseVisible = true;
                switch(_gameMenu.Update(gameTime))
                {
                    case GameMenuState.NewGame:
                        _countdown = 10.0f;
                        _levelManager.SetLevel(0);
                        _gameState = GameState.PlayGame;
                        IsMouseVisible = false;
                    break;
                    case GameMenuState.Quit:
                        Exit();
                    break;
                }
            break;
            case GameState.PlayGame:
                if(_levelManager.Update(gameTime,_input))
                {
                    if(!_alitsa.IsActive && !_atsin.IsActive)
                    {
                        _gameState = GameState.GameOver;
                    }
                }else
                {
                    _gameState = GameState.EndOfAllLevels;
                }
            break;
            case GameState.EndOfAllLevels:
                _countdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(_countdown <= 0.0f)
                {
                    _gameState = GameState.GameMenu;
                }
            break;
            case GameState.GameOver:
                _countdown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(_countdown <= 0.0f)
                {
                    _gameState = GameState.GameMenu;
                }
            break;
        }
        base.Update(gameTime);
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        
        switch(_gameState)
        {
            case GameState.GameMenu:
                _gameMenu.Draw(_spriteBatch);
            break;
            case GameState.PlayGame:
                _levelManager.Draw(_spriteBatch);
                _gameHUD.Draw(_spriteBatch);
            break;
            case GameState.EndOfAllLevels:
                _background.Draw(_spriteBatch);
                string textThanks = "Thank You for play";
                var textThanksSize = Global.DefaultFont.MeasureString(textThanks);
                _spriteBatch.DrawString(Global.DefaultFont,textThanks,
                                        new Vector2((Global.ScreenWidth-textThanksSize.X)/2,
                                                    (Global.ScreenHeight-textThanksSize.Y)/2),
                                        Color.White);
            break;
            case GameState.GameOver:
                _levelManager.Draw(_spriteBatch);
                _gameHUD.Draw(_spriteBatch);
                string textGameOver = "Both player is dead";
                var textGameOverSize = Global.DefaultFont.MeasureString(textGameOver);
                _spriteBatch.DrawString(Global.DefaultFont,textGameOver,
                                        new Vector2((Global.ScreenWidth-textGameOverSize.X)/2,
                                                    (Global.ScreenHeight-textGameOverSize.Y)/2),
                                        Color.White);
            break;
        }
	    _spriteBatch.End();
        base.Draw(gameTime);
    }
}
