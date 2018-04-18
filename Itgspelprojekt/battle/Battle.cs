
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

        //Tors kod som kopplar battle
        class Battle
        {
            animationForBattle battleAnimation, battleMenuAnimation, battleHealthbars;
            Texture2D battle, menuBattle, healthMenuBattle;
            controlForUI mainBattleMenu;
            List<UI> UIList;
            SpriteFont spriteFont;

            public Battle(Texture2D backround, Texture2D inventoryMenu, Texture2D healthMenu, List<UI> listOfUI, SpriteFont spriteFont)
            {
                //texture load
                battle = backround;
                menuBattle = inventoryMenu;
                healthMenuBattle = healthMenu;
                this.spriteFont = spriteFont;

                UIList = listOfUI;
                mainBattleMenu = new controlForUI(spriteFont, new Vector2(740, 550), 2, 2);

            //battle animationer
            battleAnimation = new animationForBattle(battle, new Vector2(0, 0), new Vector2(0, 0));
                battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200, 1200), new Vector2(-1, -1));
                battleHealthbars = new animationForBattle(healthMenuBattle, new Vector2(1200, 0), new Vector2(-1, 60));
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



