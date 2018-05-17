
using Itgspelprojekt.Creatures;
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
    class PlayerssTurn : NormalBattle
    {

        //Tors kod
        public PlayerssTurn(Texture2D background, Texture2D inventoryMenu, Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI, Player player, SpriteBatch spriteBatch, SpriteFont spriteFont) : base(background, inventoryMenu, healthMenu, menyn, listOfUI, player, spriteFont, spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();

            //menyn
            mainBattleMenu.Update(gameTime);


            if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
            {
                // Anfaller
                if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 1 && mainBattleMenu.SelectorPositionY == 1))
                {
                    if (Game1.battleOpponent.Health <= 0)
                    {
                        Game1.gamestate = Gamestate.ingame;
                        Game1.battleOpponent.canDoBattle = false;
                    }
                    else
                    {
                        Attack.AttackOnEnemy(player, Game1.battleOpponent);
                        turn = Turn.middle;
                        lastAction = LastAction.Pattack;
                    }

                }
                //flyr från stidern
                else if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 2))
                {
                    //ändrar gamestate så det blir tillbacka i spelet
                    Game1.gamestate = Gamestate.ingame;
                    Game1.battleOpponent.canDoBattle = false;


                }
                // inventory should go in here
                else if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 1))
                {

                }
                //get information about the creature
                else if ((Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 1 && mainBattleMenu.SelectorPositionY == 2))
                {

                    lastAction = LastAction.stats;
                    turn = Turn.middle;

                }
            }

            oldState = newState;




        }




    }
}
