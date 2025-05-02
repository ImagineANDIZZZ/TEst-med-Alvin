using System.Drawing;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace TEst_med_Alvin
{
    public class Bullet_Bill
    {
        private Texture2D texture;
        private Microsoft.Xna.Framework.Vector2 position;
        private Microsoft.Xna.Framework.Rectangle hitbox;
        public Microsoft.Xna.Framework.Rectangle Hitbox{
            get{return hitbox;}
        }


        public Bullet_Bill(Texture2D texture){
            this.texture = texture;
            hitbox = new Microsoft.Xna.Framework.Rectangle(800,50,50,50);
            position = hitbox.Location.ToVector2();
        }

        public void Update(){
            float speed = 60;
            position.X -= speed *1/60f;
            hitbox.Location = position.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Microsoft.Xna.Framework.Color.White);
        }
    }
}