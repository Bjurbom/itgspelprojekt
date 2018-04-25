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
    class NormalBattle : Battle
    {
        public NormalBattle(Texture2D background,Texture2D inventoryMenu,Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI) : base(background,inventoryMenu,healthMenu,menyn,listOfUI)
        {

        }

        public void Update(Camera camera, GameTime gameTime)
        {
            camera.Update(new Vector2(battle.Width / 2, battle.Height / 2));
            camera.Zoom = 1;
            camera.Rotation = 0;

            battleAnimation.Update(gameTime);
            battleMenuAnimation.Update(gameTime);
            battleHealthbars.Update(gameTime);

            if (battleHealthbars.InPosition == true)
            {
                mainBattleMenu.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 2)
            {
                Game1.gamestate = Gamestate.ingame;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            battleAnimation.Draw(spriteBatch);
            battleMenuAnimation.Draw(spriteBatch);
            battleHealthbars.Draw(spriteBatch);

            if (battleMenuAnimation.InPosition == true)
            {
                foreach (UI textItem in UIList)
                {
                    textItem.Draw(spriteBatch);
                }

                mainBattleMenu.Draw(spriteBatch);
            }
        }
    }
}
