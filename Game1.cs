using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using SharpDX.Direct3D9;
using System;

namespace TEst_med_Alvin;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D Supermario;
    private Texture2D BrickPlatform;
    private Platform platform;
    private Brick box;
    private Cloud box;
    private Texture2D brick;
    private Texture2D cloud;
    private Texture2D Fireball;
    private Texture2D Goomba;
    private List<Brick> boxar = new List<Brick>();
    private List<Cloud> boxar = new List<Cloud>();
    private List<Goomba> goombas = new List<Goomba>();
    Song theme;
    SoundEffect effect;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    private int Hp = 1;

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        effect = Content.Load<SoundEffect>("jump_07");
        Supermario = Content.Load<Texture2D>("supermario");
        BrickPlatform = Content.Load<Texture2D>("BrickPlatform");
        platform = new Platform (BrickPlatform,new Vector2(0, 350),new Vector2(660,260));
        brick = Content.Load<Texture2D>("Brick");
        cloud = Content.Load<Texture2D>("Cloud");
        Fireball = Content.Load<Texture2D>("Fireball");
        AddBricks();
        addClouds();
        theme = Content.Load<Song>("theme");
        MediaPlayer.Play(theme);
        Goomba = Content.Load<Texture2D>("Goomba");
        player = new Player (Supermario,new Vector2(380, 350),50, effect, Fireball);
    }
    

    protected override void Update(GameTime gameTime)
    {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update();
        playerbrickcollision();
        playercloudcollision();
        base.Update(gameTime);
        foreach(Goomba goomba in goombas){
            goomba.Update();
        }
        GoombaFireballCollision();
        GoombaSupermariolCollision();
        SpawnGoomba();
    }

    protected override void Draw(GameTime gameTime)
    {
        Rectangle bgRect = new(0,0,800,600);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        player.Draw(_spriteBatch);
        platform.Draw(_spriteBatch);
        foreach(Brick b in boxar){
            b.Draw(_spriteBatch);
        }
        foreach(Cloud c in boxar){
            b.Draw(_spriteBatch);
        }
        foreach(Goomba goomba in goombas){
        goomba.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void AddBricks(){
            boxar.Add(new Brick (brick,new Vector2(250, 150),new Vector2(40,40)));        
            boxar.Add(new Brick (brick,new Vector2(500, 200),new Vector2(40,40)));   
            boxar.Add(new Brick (brick,new Vector2(40, 60),new Vector2(40,40)));    
    }

    private void playerbrickcollision(){
        foreach(Brick b in boxar){
            if(b.Hitbox.Intersects(player.Hitbox)){
                player.BrickCollision();
            }
        }      
    }

    private void AddClouds(){  
            boxar.Add(new Cloud (cloud,new Vector2(300, 120),new Vector2(40,40)));    
    }

    private void playercloudcollision(){
        foreach(Cloud c in boxar){
            if(b.Hitbox.Intersects(player.Hitbox)){
                player.CloudCollision();
            }
        }      
    } 

    private void SpawnGoomba(){
        Random rand = new Random();
        int value = rand.Next(1,100);
        int spawnChanceProcent = 1;
        if(value <= spawnChanceProcent)
            goombas.Add(new Goomba(Goomba));
    }

    private void GoombaFireballCollision(){
        for(int i = 0; i < goombas.Count; i++){
            for(int j =0; j < player.Fireballs.Count; j++){
                if(goombas[i].Hitbox.Intersects(player.Fireballs[j].Hitbox)){
                    goombas.RemoveAt(i);
                    player.Fireballs.RemoveAt(j);
                    break;
                }
            }
        }
    }

    private void GoombaSupermariolCollision(){
        for(int i = 0; i < goombas.Count; i++){
            if(goombas[i].Hitbox.Intersects(player.Hitbox)){
                Hp--;
                 goombas.RemoveAt(i);
                i--;
                if(Hp <= 0){
                    Exit();
                }
            }
        }
    }
}
