using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.Map_generator
{
    public abstract class Tiless
    {
        // Tors abstracta bas klass

        //variabler /m content
        protected Texture2D texture;
        private Rectangle rectangle;
        private static ContentManager content;
        protected int id;

        //inkappsling

        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }

        }
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //ritar
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
