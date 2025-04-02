
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TEst_med_Alvin
{
    public class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
        private SoundEffect jumpsound;
        public Rectangle Hitbox{
            get{return hitbox;}
        }
        const float GRAVITY = 18.4f;
        Vector2 velocity;
        private bool canJump = true;
        private List<Fireball> fireballs = new List<Fireball>();
        public List<Fireball> Fireballs{
            get{return fireballs;}
        }

        public Player(Texture2D texture, Vector2 position, int pixelsize, SoundEffect jumpsound){
            this.texture = texture;
            this.position = position;
            this.jumpsound = jumpsound;
            hitbox = new Rectangle((int)position.X,(int)position.Y,pixelsize,pixelsize);
        }
        private void Jump(){
                velocity.Y = -10;
                canJump = false;

        }
        public void Update(){
            KeyboardState Kstate = Keyboard.GetState();

            if(Kstate.IsKeyDown(Keys.A)){
                position.X -= 3;
            }
            else if(Kstate.IsKeyDown(Keys.D)){
                position.X += 3;
            }
            if(Kstate.IsKeyDown(Keys.Space)){
                if(canJump){
                    Jump();
                    jumpsound.Play();
                }
            }
            if(position.Y > 300){
                velocity.Y = 0;
                position.Y = 300;
                canJump = true;
            }
            position.Y += velocity.Y;
            velocity.Y += GRAVITY * 1f/60f; 
            hitbox.Location = position.ToPoint();
            

            foreach(Fireball f in fireballs){
                f.Update();
            }
        }
        private void Shoot(){
            MouseState Mstate = Mouse.GetState();
            if(Mstate.LeftButton==ButtonState.Pressed){
                Fireball fireball = new Fireball(texture,position);
                fireballs.Add(fireball);
            }
        } 
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
            foreach(Fireball f in fireballs){
                f.Draw(spriteBatch);
            }
        }
        public void BrickCollision(){
            velocity.Y = 0;
        }
    }
}