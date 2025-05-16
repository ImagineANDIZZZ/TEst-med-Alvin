using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;

namespace TEst_med_Alvin
{
    public class Platform
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle hitbox;
         public Rectangle Hitbox{
            get{
                if(rotation ==0)
                    return hitbox;
                return new Rectangle(position.ToPoint() - new Point(hitbox.Height/2,hitbox.Width/2), new Point(hitbox.Height,hitbox.Width));
            }
        }
        private float rotation = 0;
        private Vector2 rotationOrigo = Vector2.Zero;
        private Platform platform;


    
        public Platform(Texture2D texture, Vector2 position, Vector2 size,  Vector2 rotationOrigo,float startRotation = 0 ){
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle(position.ToPoint(),size.ToPoint());
            this.rotationOrigo = rotationOrigo;
            this.rotation = startRotation;
        }    
        public void Draw(SpriteBatch spriteBatch){
            
            spriteBatch.Draw(texture,hitbox, null,Color.White,rotation,rotationOrigo,SpriteEffects.None,0);
        }
        
    }
    
}

