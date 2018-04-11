using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Itgspelprojekt.battle
{
    class UI

        // Tors UI kod för bara texten
    {
        private SpriteFont font;
        private Vector2 position;
        private string text;

        public UI(Vector2 position, SpriteFont font, string text)
        {
            this.position = position;
            this.font = font;
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.Black);
        }



    }
}
