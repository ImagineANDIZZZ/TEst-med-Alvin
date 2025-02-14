
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TEst_med_Alvin
{
    public class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
        const float GRAVITY = 9.82f;
        Vector2 velocity;

        public Player(Texture2D texture, Vector2 position, int pixelsize){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int)position.X,(int)position.Y,pixelsize,pixelsize);
        }

        public void Update(){
            KeyboardState Kstate = Keyboard.GetState();

            if(Kstate.IsKeyDown(Keys.A)){
                position.X -= 1;
            }
            else if(Kstate.IsKeyDown(Keys.D)){
                position.X += 1;
            }
            if(Kstate.IsKeyDown(Keys.Space)){
                velocity.Y = -100;
            }
            position.Y += velocity.Y;
            velocity.Y += GRAVITY * 1f/60f; 
            hitbox.Location = position.ToPoint();

        }
    }

}