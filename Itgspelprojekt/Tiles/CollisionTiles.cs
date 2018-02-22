using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.Tiles
{
    class CollisionTiles : Tiless
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            this.texture = Content.Load<Texture2D>("tile" + 1); 
            this.Rectangle = newRectangle;
        }
    }
}
