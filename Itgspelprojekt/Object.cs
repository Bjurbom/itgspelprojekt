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
        // Tommies oanvända Object klass
        Vector2 position;
        public Texture2D texture;
        public Rectangle hitbox; // will be set to texture.Bounds in the constructor
        public string name;
        
        public void Creature(string name, Vector2 position, Texture2D texture)
        {
            this.name = name;
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
