
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Creatures;

namespace Itgspelprojekt.battle
    {

        

        //Tors kod som kopplar battle
        class Battle
        {
            enum MenuState { MainMenu, Attack, Stats}
            
            
            animationForBattle battleAnimation, battleMenuAnimation, battleHealthbars;
            Texture2D battle, menuBattle, healthMenuBattle, whiteBlock;
            MenuState state;
            controlForUI mainBattleMenu;
            List<UI> UIList;
            SpriteFont spriteFont;
            Rectangle health;
            

            public Battle(Texture2D backround, Texture2D inventoryMenu, Texture2D healthMenu, List<UI> listOfUI, SpriteFont spriteFont, Texture2D whiteBlock)
            {
                //texture load
                battle = backround;
                menuBattle = inventoryMenu;
                healthMenuBattle = healthMenu;
                this.spriteFont = spriteFont;
                this.whiteBlock = whiteBlock;

        
                //laddar in övriga saker
                UIList = listOfUI;
                mainBattleMenu = new controlForUI(spriteFont, new Vector2(740, 550), 2, 2);
                state = MenuState.MainMenu;

                //battle animationer
                battleAnimation = new animationForBattle(battle, new Vector2(0, 0), new Vector2(0, 0));
                battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200, 1200), new Vector2(-1, -1));
                battleHealthbars = new animationForBattle(healthMenuBattle, new Vector2(1200, 0), new Vector2(-1, 60));

            }

            public void Update(Camera camera, GameTime gameTime, Player player)
            {
                camera.Update(new Vector2(battle.Width / 2, battle.Height / 2));
                camera.Zoom = 1;
                camera.Rotation = 0;

                

                battleAnimation.Update(gameTime);
                battleMenuAnimation.Update(gameTime);
                battleHealthbars.Update(gameTime);
            if (state == MenuState.MainMenu)
            {
                if (battleHealthbars.InPosition == true)
                {
                    health = new Rectangle(30, 30, player.Health, 30);
                    mainBattleMenu.Update(gameTime);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 2 && mainBattleMenu.SelectorPositionY == 2)
                {
                    Game1.gamestate = Gamestate.ingame;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 1 && mainBattleMenu.SelectorPositionY == 1)
                {

                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && mainBattleMenu.SelectorPositionX == 1 && mainBattleMenu.SelectorPositionY == 2)
                {

                }

            }


        }

            public void Draw(SpriteBatch spriteBatch)
            {


                battleAnimation.Draw(spriteBatch);
                battleMenuAnimation.Draw(spriteBatch);
                battleHealthbars.Draw(spriteBatch);

            if (state == MenuState.MainMenu)
            {
                if (battleMenuAnimation.InPosition == true)
                {
                    spriteBatch.Draw(whiteBlock, health, Color.Green);

                    foreach (UI textItem in UIList)
                    {
                        textItem.Draw(spriteBatch);
                    }

                    mainBattleMenu.Draw(spriteBatch);
                }
            }
          

                
            }

        }
    }



