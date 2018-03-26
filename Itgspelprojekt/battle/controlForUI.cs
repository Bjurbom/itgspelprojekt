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
        private double elapsedTime, interval;

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
            selectorPositionY = 1;
            this.font = font;
            interval = 150;
        }

        public void Update(GameTime gameTime)
        {
            if (elapsedTime > interval)
            {

                elapsedTime -= interval;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (selectorPositionY != 1)
                    {
                        selectorPosition.Y -= 50;
                        selectorPositionY--;
                    }

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (selectorPositionY != sizeY)
                    {
                        selectorPosition.Y += 50;
                        selectorPositionY++;
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (selectorPositionX != 1)
                    {
                        selectorPosition.X -= 100;
                        selectorPositionX--;
                    }

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (selectorPositionX != sizeX)
                    {
                        selectorPosition.X += 100;
                        selectorPositionX++;
                    }

                }
            }

            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, ">", selectorPosition, Color.Black);

            spriteBatch.DrawString(font, Convert.ToString(selectorPositionX), new Vector2(300, 300), Color.Black);
            spriteBatch.DrawString(font, Convert.ToString(selectorPositionY), new Vector2(300, 350), Color.Black);
        }
    }
}
