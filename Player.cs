
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



        public void update(){
            KeyboardState Kstate = Keyboard.GetState();

            if(Kstate.IsKeyDown(Keys.A)){
                position.X -= 1;
            }
            else if(Kstate.IsKeyDown(Keys.D)){
                position.X += 1;
            } 
            hitbox.Location = position.ToPoint();

        }
    }

}