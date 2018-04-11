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
    // Tors collision tiles
    class CollisionTiles : Tiless
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {
            //get texture med namnet tile plus ett nummer i slutet
            this.texture = Content.Load<Texture2D>("tile" + i);

            //lägger in rectangle på property i tiles
 
            
            this.Rectangle = newRectangle;
            


            this.id = i;
        }
    }
}
