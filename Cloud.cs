using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin
{
    public class Cloud
    {
       private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
        private Rectangle drawRec; 
        public Rectangle Hitbox{
            get{return hitbox;}
        }

         public Cloud(Texture2D texture, Vector2 position, Vector2 size){
            this.texture = texture;
            this.position = position;
            drawRec = new Rectangle(position.ToPoint(),size.ToPoint());
            hitbox = new Rectangle(position.ToPoint(),size.ToPoint());
            hitbox.Height/=2;
            hitbox.Y += hitbox.Height;
        }    
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, drawRec, Color.White);
        }
    }
    
}