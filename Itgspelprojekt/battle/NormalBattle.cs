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
        enum Turn{player, enemey }

        static Turn turn;
        PlayersTurn playersTurn;

        public NormalBattle(Texture2D background,Texture2D inventoryMenu,Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI) : base(background,inventoryMenu,healthMenu,menyn,listOfUI)
        {
            //gör spelar börjar i striden
            turn = Turn.player;
        }

        /// <summary>
        /// Denna update metod gör animation i början av battle medans den håller reda
        /// på vems tur det är i striden
        /// spelaren böjar i striden
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="gameTime"></param>
        public void Update(Camera camera, GameTime gameTime) 
        {
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
                //meny fysiken
                mainBattleMenu.Update(gameTime);

                //om spelaren tur så skapas objecte samt kör update
                if (turn == Turn.player)
                {
                    playersTurn = new PlayersTurn(battleTexture, menuBattle, healthMenuBattle, mainBattleMenu,UIList);
                    playersTurn.Update();
                }

            }

        }

        /// <summary>
        /// ritar ut allt som är inneblandad i denna klass
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //ritar ut animationerna / spritesen
            battleAnimation.Draw(spriteBatch);
            battleMenuAnimation.Draw(spriteBatch);
            battleHealthbars.Draw(spriteBatch);

            //ritar ut all text
            if (battleMenuAnimation.InPosition == true)
            {
                foreach (UI textItem in UIList)
                {
                    textItem.Draw(spriteBatch);
                }

                //ritar ut backrounden som inte rör på sig
                mainBattleMenu.Draw(spriteBatch);
            }
        }
    }
}
