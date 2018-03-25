using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.battle
{
    class controlForUI
    {
        private int selectorPositionY;
        private Vector2 selectorPosition;
        private int selectorPositionX;
        private int sizeX, sizeY;
        private SpriteFont font;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionForCursior"> position för var pekaren ska va</param>
        /// <param name="sizeX"> storlek med X axen</param>
        /// <param name="sizeY"> storlek med Y axen</param>
        public controlForUI(SpriteFont font,Vector2 positionForCursior, int sizeX, int sizeY)
        {
            selectorPosition = positionForCursior;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            selectorPositionX = 1;
            selectorPositionY = 2;
            this.font = font;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (selectorPositionY != sizeY)
                {
                    selectorPosition.Y -= 100;
                    selectorPositionY++;
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (selectorPositionY != 1)
                {
                    selectorPosition.Y += 100;
                    selectorPositionY--;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (selectorPositionX != 1)
                {
                    selectorPosition.X -= 250;
                    selectorPositionX--;
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (selectorPositionX != sizeX)
                {
                    selectorPosition.X += 250;
                    selectorPositionX++;
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, ">", selectorPosition, Color.Black);
        }
    }
}
