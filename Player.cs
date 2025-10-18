
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
        private Texture2D Fireball;
        private MouseState oldMouseState;
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

        public Player(Texture2D texture, Vector2 position, int pixelsize, SoundEffect jumpsound,Texture2D Fireball){
            this.texture = texture;
            this.position = position;
            this.jumpsound = jumpsound;
            hitbox = new Rectangle((int)position.X,(int)position.Y,50,50);
            this.Fireball = Fireball;
        }
        private void Jump(){
                velocity.Y = -10;
                canJump = false;

        }
        public void Update(){
            KeyboardState Kstate = Keyboard.GetState();
            velocity.X = 0;
            if(Kstate.IsKeyDown(Keys.A)){
                velocity.X = -3;
            }
            else if(Kstate.IsKeyDown(Keys.D)){
                velocity.X = 3;
            }
            if(Kstate.IsKeyDown(Keys.Space)){
                if (canJump){
                    Jump();
                    jumpsound.Play();
                }
            }
            
            position += velocity;
            velocity.Y += GRAVITY * 1f/60f;

            canJump = false;
            if (position.Y > 300)
            {
                velocity.Y = 0;
                position.Y = 300;
                canJump = true;
            }
            hitbox.Location = position.ToPoint();

                
            Shoot();

            foreach(Fireball f in fireballs){
                f.Update();
            }
        }
        private void Shoot(){
            MouseState Mstate = Mouse.GetState();
            if(Mstate.LeftButton==ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released){
                Fireball fireball = new Fireball(Fireball,position + new Vector2(44,20));
                fireballs.Add(fireball);
            }
            oldMouseState = Mstate;
        } 
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
            foreach(Fireball f in fireballs){
                f.Draw(spriteBatch);
            }
        }
        public void BrickCollision(Rectangle brickhitbox){
            float lastYPos = position.Y - velocity.Y;
            position.Y = brickhitbox.Y + brickhitbox.Height;
            if(lastYPos + hitbox.Height < brickhitbox.Y){
                velocity.Y = 0;
                position.Y = brickhitbox.Y - hitbox.Height-2; 
                canJump=true;
            }
        }
        public void BCollision(Rectangle brickhitbox){
            Vector2 prevPos = position;
            prevPos.X -= velocity.X;
            hitbox.Location = prevPos.ToPoint();
            if(!hitbox.Intersects(brickhitbox))
            {
                position.X= prevPos.X;
            }
            else{
                position.Y -= velocity.Y;
                if(velocity.Y >0){
                    canJump =true;
                    position.Y = brickhitbox.Top - hitbox.Height;
                }
                else
                    position.Y = brickhitbox.Bottom;
                velocity.Y = 0; 
            }
            hitbox.Location = position.ToPoint();
        }

        public void CloudCollision(Rectangle cloudhitbox){
            float lastYPos = position.Y - velocity.Y;
            position.Y = cloudhitbox.Y + cloudhitbox.Height;
            if(lastYPos + hitbox.Height < cloudhitbox.Y){
                velocity.Y = 0;
                position.Y = cloudhitbox.Y - hitbox.Height-2; 
                canJump=true;
            }
        }
         public void CCollision(Rectangle cloudhitbox){
            Vector2 prevPos = position;
            prevPos.X -= velocity.X;
            hitbox.Location = prevPos.ToPoint();
            if(!hitbox.Intersects(cloudhitbox))
            {
                position.X= prevPos.X;
            }
            else{
                position.Y -= velocity.Y;
                if(velocity.Y >0){
                    canJump =true;
                    position.Y = cloudhitbox.Top - hitbox.Height;
                }
                else
                    position.Y = cloudhitbox.Bottom;
                velocity.Y = 0; 
            }
            hitbox.Location = position.ToPoint();
        }

        public void PlatformCollision(Rectangle cloudhitbox){
            float lastYPos = position.Y - velocity.Y;
            position.Y = cloudhitbox.Y + cloudhitbox.Height;
            if(lastYPos + hitbox.Height < cloudhitbox.Y){
                velocity.Y = 0;
                position.Y = cloudhitbox.Y - hitbox.Height-2; 
                canJump=true;
            }
        }
         public void PCollision(Rectangle cloudhitbox){
            Vector2 prevPos = position;
            prevPos.X -= velocity.X;
            hitbox.Location = prevPos.ToPoint();
            if(!hitbox.Intersects(cloudhitbox))
            {
                position.X= prevPos.X;
            }
            else{
                position.Y -= velocity.Y;
                if(velocity.Y >0){
                    canJump =true;
                    position.Y = cloudhitbox.Top - hitbox.Height;
                }
                else
                    position.Y = cloudhitbox.Bottom;
                velocity.Y = 0; 
            }
            hitbox.Location = position.ToPoint();
        }
    }
}