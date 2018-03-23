using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.battle
{
    class animationForBattle
    {
        private Texture2D sprite;
        private Vector2 endPosition;
        private Vector2 currentPosition;

        public animationForBattle(Texture2D sprite, Vector2 startPosition, Vector2 endPosition)
        {
            this.sprite = sprite;
            this.endPosition = endPosition;
            currentPosition = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.ElapsedGameTime.TotalMilliseconds >= 500)
            {
                currentPosition += endPosition;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, currentPosition, Color.White);
        }


    }
}
