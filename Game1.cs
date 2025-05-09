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
    private Texture2D brickTexture;
    private Brick box;
    //private Platform Platform;
    //private Texture2D brickTexture;
    private Texture2D brick;
    private Texture2D cloud;
    private Texture2D Fireball;
    private Texture2D Goomba;
    private Texture2D Bullet_Bill;
    private List<Brick> boxar = new List<Brick>();
    private List<Platform> Platforms = new List<Platform>();
    private List<Cloud> clouds = new List<Cloud>();
    private List<Goomba> goombas = new List<Goomba>();
    private List<Bullet_Bill> bullet_Bills = new List<Bullet_Bill>();
    Song theme;
    SoundEffect effect;
    Camera cam ;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    private int Hp = 1;

    protected override void Initialize()
    {
        cam = new Camera(GraphicsDevice.Viewport);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        effect = Content.Load<SoundEffect>("jump_07");
        Supermario = Content.Load<Texture2D>("supermario");
        brickTexture = Content.Load<Texture2D>("BrickPlatform");
        //Platform = new Platform (brickTexture,new Vector2(0, 350),new Vector2(660,260));
        brick = Content.Load<Texture2D>("Brick");
        cloud = Content.Load<Texture2D>("Cloud");
        Fireball = Content.Load<Texture2D>("Fireball");
        AddBricks();
        AddClouds();
        theme = Content.Load<Song>("theme");
        MediaPlayer.Play(theme);
        Goomba = Content.Load<Texture2D>("Goomba");
        Bullet_Bill = Content.Load<Texture2D>("bullet-bill");
        player = new Player (Supermario,new Vector2(380, 350),50, effect, Fireball);
        cam = new Camera(GraphicsDevice.Viewport);
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

        foreach(Bullet_Bill bullet_Bill in bullet_Bills){
            bullet_Bill.Update();
        }
        Bullet_BillFireballCollision();
        Bullet_BillSupermariolCollision();
        SpawnBullet_Bill();

        AddPlatform();
        playerPlatformcollision();
        cam.UpdateCamera(GraphicsDevice.Viewport,player.Hitbox.Location.ToVector2());
    }

    protected override void Draw(GameTime gameTime)
    {
        Rectangle bgRect = new(0,0,800,600);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin(transformMatrix: cam.Transform);
        player.Draw(_spriteBatch);
        //Platform.Draw(_spriteBatch);
        foreach(Brick b in boxar){
            b.Draw(_spriteBatch);
        }
        foreach(Cloud c in clouds){
            c.Draw(_spriteBatch);
        }
        foreach(Goomba goomba in goombas){
        goomba.Draw(_spriteBatch);
        }
        foreach(Bullet_Bill bullet_bill in bullet_Bills){
        bullet_bill.Draw(_spriteBatch);
        }
         foreach(Platform p in Platforms){
            p.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void AddBricks(){
            boxar.Add(new Brick (brick,new Vector2(280, 160),new Vector2(40,40)));
            boxar.Add(new Brick (brick,new Vector2(240, 160),new Vector2(40,40))); 
            boxar.Add(new Brick (brick,new Vector2(200, 160),new Vector2(40,40))); 
            boxar.Add(new Brick (brick,new Vector2(80, 240),new Vector2(40,40))); 
            boxar.Add(new Brick (brick,new Vector2(460, 240),new Vector2(40,40)));           
            boxar.Add(new Brick (brick,new Vector2(500, 240),new Vector2(40,40)));   
            boxar.Add(new Brick (brick,new Vector2(540, 240),new Vector2(40,40)));
            boxar.Add(new Brick (brick,new Vector2(660, 240),new Vector2(40,40)));    
            boxar.Add(new Brick (brick,new Vector2(700, 240),new Vector2(40,40)));        
    }

    private void playerbrickcollision(){
        foreach(Brick b in boxar){
            if(b.Hitbox.Intersects(player.Hitbox)){
                player.BCollision(b.Hitbox);
            }
        }      
    }

    private void AddClouds(){
            clouds.Add(new Cloud (cloud,new Vector2(450, 20),new Vector2(60,60)));
            clouds.Add(new Cloud (cloud,new Vector2(40, 80),new Vector2(60,60)));  
            clouds.Add(new Cloud (cloud,new Vector2(720, 80),new Vector2(60,60)));      
    }

    private void playercloudcollision(){
        foreach(Cloud c in clouds){
            if(c.Hitbox.Intersects(player.Hitbox)){
                player.CCollision(c.Hitbox);
            }
        }      
    }

    private void AddPlatform(){
            Platforms.Add(new Platform (brickTexture,new Vector2(-620, 350),new Vector2(600,190),Vector2.Zero));
            Platforms.Add(new Platform (brickTexture,new Vector2(-20, 350),new Vector2(600,190),Vector2.Zero));
            Platforms.Add(new Platform (brickTexture,new Vector2(580, 350),new Vector2(600,190),Vector2.Zero));
            Platforms.Add(new Platform (brickTexture,new Vector2(1180, 350),new Vector2(600,190),Vector2.Zero));  
            Platforms.Add(new Platform (brickTexture,new Vector2(-700, -200),new Vector2(200,400), new Vector2(brickTexture.Width,brickTexture.Height)/2,MathHelper.PiOver2));      
    }

    private void playerPlatformcollision(){
        foreach(Platform p in Platforms){
            if(p.Hitbox.Intersects(player.Hitbox)){
                player.PCollision(p.Hitbox);
            }
        }      
    } 


    private void SpawnGoomba(){
        Random rand = new Random();
        int value = rand.Next(1,200);
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


    private void SpawnBullet_Bill(){
        Random rand = new Random();
        int value = rand.Next(1,200);
        int spawnChanceProcent = 1;
        if(value <= spawnChanceProcent)
            bullet_Bills.Add(new Bullet_Bill(Bullet_Bill));
    }

    private void Bullet_BillFireballCollision(){
        for(int i = 0; i < bullet_Bills.Count; i++){
            for(int j =0; j < player.Fireballs.Count; j++){
                if(bullet_Bills[i].Hitbox.Intersects(player.Fireballs[j].Hitbox)){
                    bullet_Bills.RemoveAt(i);
                    player.Fireballs.RemoveAt(j);
                    break;
                }
            }
        }
    }

    private void Bullet_BillSupermariolCollision(){
        for(int i = 0; i < bullet_Bills.Count; i++){
            if(bullet_Bills[i].Hitbox.Intersects(player.Hitbox)){
                Hp--;
                 bullet_Bills.RemoveAt(i);
                i--;
                if(Hp <= 0){
                    Exit();
                }
            }
        }
    }
}
