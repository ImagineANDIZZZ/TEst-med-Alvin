using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D Supermario;
    private Texture2D Grass;
    private Platform platform;
    private Texture2D bakgrundsbild;
    private Texture2D box;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Supermario = Content.Load<Texture2D>("supermario");
        Grass = Content.Load<Texture2D>("grass");
        player = new Player (Supermario,new Vector2(380, 350),50);
        platform = new Platform (Grass,new Vector2(0, 350),new Vector2(830,130));
        bakgrundsbild = Content.Load<Texture2D>("bakgrundsbild");
        box = Content.Load<Texture2D>("brick");
        brick = new Brick (Grass,new Vector2(0, 350),new Vector2(830,130));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Rectangle bgRect = new(0,0,800,600);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(bakgrundsbild, bgRect, Color.White);
        player.Draw(_spriteBatch);
        platform.Draw(_spriteBatch);
        _spriteBatch.End();
        base.Draw(gameTime);
        
    }
}
