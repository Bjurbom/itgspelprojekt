using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Itgspelprojekt
{
    class Object
    {
        bool canBePickedUp, hasCollisions, isPushable;
        Vector2 position;
        public Texture2D texture;
        public Rectangle hitbox; // will be set to texture.Bounds in the constructor
        public string ID;
        
        public void Creature(string ID, Vector2 position, Texture2D texture)
        {
            this.ID = ID;
            this.position = position;
            this.texture = texture;
            this.hitbox = texture.Bounds;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
