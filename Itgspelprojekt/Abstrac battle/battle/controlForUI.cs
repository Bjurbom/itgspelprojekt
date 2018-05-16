using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.Abstrac_battle.battle
{

    // Tors controll för UI kod.

    class controlForUI
    {
        protected int selectorPositionY;
        protected Vector2 selectorPosition;
        protected int selectorPositionX;
        protected int sizeX, sizeY;
        protected SpriteFont font;
        protected double elapsedTime, interval;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionForCursior"> position för var pekaren ska va</param>
        /// <param name="sizeX"> storlek med X axen</param>
        /// <param name="sizeY"> storlek med Y axen</param>
        public controlForUI(SpriteFont font,Vector2 positionForCursior, int sizeX, int sizeY)
        {

            //start positionen för pekaren i koodinatsystemetr
            selectorPosition = positionForCursior;

            // storlek för menyn
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            // start positionen
            selectorPositionX = 1;
            selectorPositionY = 1;
            this.font = font;

            //hur lång delay hur ofta man upptaterar pekaren
            interval = 150;
        }

        // properties för var pekare är
        #region
        public int SelectorPositionX
        {
            get
            {
                return selectorPositionX ;
            }
        }

        public int SelectorPositionY
        {
            get
            {
                return selectorPositionY;
            }
        }
        #endregion

        /// <summary>
        /// Denna klass upptaterar pekaren samt ändrar positionen för den. 
        /// Samt så Uptateras saktar på grund av Interval setting
        /// </summary>
        /// <param name="gameTime"></param>
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

            
            // upptaterar klockan med milli sekunder
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

        }

        // ritar ut pekaren
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, ">", selectorPosition, Color.Black);

            //Temp
            spriteBatch.DrawString(font, Convert.ToString(selectorPositionX), new Vector2(300, 300), Color.Black);
            spriteBatch.DrawString(font, Convert.ToString(selectorPositionY), new Vector2(300, 350), Color.Black);
        }
    }
}
