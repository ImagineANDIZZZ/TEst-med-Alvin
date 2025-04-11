using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace TEst_med_Alvin
{
    public class Fireball
    {
        private Texture2D texture;
        private Vector2 position;
        private float startY;
        private Rectangle hitbox;
        public Rectangle Hitbox{
            get{return hitbox;}
        }
        private float t = 0;
        const float GRAVITY = 18.4f;
        public Fireball(Texture2D texture, Vector2 startPosition){
        this.texture = texture;
        position = startPosition;
        startY = startPosition.Y;
        hitbox = new Rectangle((int)position.X,(int)position.Y,22,22);
        }
        public void Update(){
            float speed = 150;
            t += MathF.PI *2 *0.016666f;
            position.Y = startY - MathF.Abs(MathF.Sin(t))*20;
            position.X += 150 * 0.01666666f;
            hitbox.Location = position.ToPoint();

        }
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
    }
}