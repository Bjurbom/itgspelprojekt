using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    enum Turn { player, enemey, middle}

    //lägg till mer funktioner som  i denna enum
    enum LastAction { Pattack, stats, healing}


    class NormalBattle : Battle
    {

        static protected LastAction lastAction;
        static protected Turn turn;

        protected KeyboardState oldState, newState;

        Healthbars playersHealthbar;
        Healthbars EnemysHealthbat;
        PlayerssTurn playersTurn;
        EnemysTurn enemysTurn;
      
        


        public NormalBattle(Texture2D background,Texture2D inventoryMenu,Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI, Player player, SpriteFont spriteFont, SpriteBatch spriteBatch) : base(background,inventoryMenu,healthMenu,menyn,listOfUI,player,spriteBatch,spriteFont)
        {
            //gör spelar börjar i striden
            turn = Turn.player;

            playersHealthbar = new Healthbars(player, new Vector2(150, 140),Color.Green);
            EnemysHealthbat = new Healthbars(Game1.battleOpponent, new Vector2(1100, 120), Color.Red);

        }

        /// <summary>
        /// Denna update metod gör animation i början av battle och grafiken medans den håller reda
        /// på vems tur det är i striden
        /// spelaren böjar i striden
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="gameTime"></param>
        public void Update(Camera camera, GameTime gameTime) 
        {
            newState = Keyboard.GetState();

            //sätter kamran till strandard
            camera.Update(new Vector2(battleTexture.Width / 2, battleTexture.Height / 2));
            camera.Zoom = 1;
            camera.Rotation = 0;

            //animationen  för battlescene spriten
            battleAnimation.Update(gameTime);
            battleMenuAnimation.Update(gameTime);
            battleHealthbars.Update(gameTime);

            //när de landar i position så kan battle sekvensen sättas igång
            if (battleHealthbars.InPosition == true)
            {
                playersHealthbar.Update(player);
                EnemysHealthbat.Update(Game1.battleOpponent);

                //om spelaren tur så skapas objecte samt kör update
                if (turn == Turn.player)
                {
                    playersTurn = new PlayerssTurn(battleTexture, menuBattle, healthMenuBattle, mainBattleMenu,UIList,player,spriteBatch,spriteFont);
                    playersTurn.Update(gameTime);
                }
                //transion till fiendens tur
                if (turn == Turn.middle)
                {
                    if (lastAction == LastAction.stats)
                    {

                        if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                        {
                            turn = Turn.enemey;
                        }
                    }
                    else if (lastAction == LastAction.Pattack)
                    {
                        if (Game1.battleOpponent.Health <= 0)
                        {
                            Game1.battleOpponent.canDoBattle = false;
                            Game1.gamestate = Gamestate.ingame;
                        }

                        //om man trycker på knappen så blir det fiendens tur
                        if (newState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                        {
                            turn = Turn.enemey;
                        }
                    }

 
                }
                if (turn == Turn.enemey)
                {
                    enemysTurn = new EnemysTurn(battleTexture, menuBattle, healthMenuBattle, mainBattleMenu, UIList, player,spriteFont,spriteBatch);
                    enemysTurn.Update(gameTime);
                }

            }

            oldState = newState;

        }

        /// <summary>
        /// ritar ut allt som är inneblandad i denna klass
        /// </summary>
        /// <param name="spriteBatch"></param>
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            //ritar ut animationerna / spritesen
            battleAnimation.Draw(spriteBatch);
            battleMenuAnimation.Draw(spriteBatch);
            battleHealthbars.Draw(spriteBatch);

            
            

            if (battleMenuAnimation.InPosition == true)
            {
                spriteBatch.DrawString(spriteFont, player.Name, new Vector2(170, 90), Color.Black);

                playersHealthbar.Draw(spriteBatch);
                EnemysHealthbat.Draw(spriteBatch);

                if (turn == Turn.player)
                {
                    mainBattleMenu.Draw(spriteBatch);

                    foreach (UI textItem in UIList)
                    {
                        textItem.Draw(spriteBatch);
                    }
                }
                else if (turn == Turn.middle)
                {
                    if (lastAction == LastAction.Pattack)
                    {
                        AttackOnEnemyDraw(spriteBatch);
                    }
                    else if (lastAction == LastAction.stats)
                    {

                    }
                }
                else if (turn == Turn.enemey)
                {
                    
                }

            }

            
          

        }

        private void AttackOnEnemyDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Du anfalde och skadade " + player.Damage,new Vector2(800,550),Color.Black);
        }
    }
}




//forsätt med att göra healthbar klassen