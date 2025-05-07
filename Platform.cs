using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TEst_med_Alvin
{
    public class Platform
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
        private Platform platform;
    
        public Platform(Texture2D texture, Vector2 position, Vector2 size){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(),size.ToPoint());
        }    
        public void Draw(SpriteBatch spriteBatch){
            spriteBatch.Draw(texture, hitbox, Color.White);
            spriteBatch.Draw(texture,new Vector2(0,0), null,Color.White,MathF.PI/2,new Vector2(texture.Width/2,texture.Height/2),1,SpriteEffects.None,0);
        }
        
    }
    
}

