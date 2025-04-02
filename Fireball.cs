using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TEst_med_Alvin
{
    public class Fireball
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle hitbox;
        public Rectangle Hitbox{
            get{return hitbox;}
        }
        public Fireball(Texture2D texture, Vector2 startPosition){
        this.texture = texture;
        position = startPosition;
        hitbox = new Rectangle((int)position.X,(int)position.Y,80,80);
        }
        public void Update(){
            float speed = 120;
            position.X += speed * 1f/60f;
            hitbox.Location = position.ToPoint();
        }
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.Blue);
        }
    }

}