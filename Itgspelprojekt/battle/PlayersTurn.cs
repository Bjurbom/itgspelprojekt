using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.battle
{
    class PlayersTurn : NormalBattle
    {
        public PlayersTurn(Texture2D background, Texture2D inventoryMenu, Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI) : base(background, inventoryMenu, healthMenu, menyn, listOfUI)
        {

        }

        public void Update()
        {
            // startar fight
            if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 1 && mainBattleMenu.SelectorPositionY == 1))
            {

            }
            //flyr från stidern
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 2)
            {
                //ändrar gamestate så det blir tillbacka i spelet
                Game1.gamestate = Gamestate.ingame;
            }
           
        }
    }
}
