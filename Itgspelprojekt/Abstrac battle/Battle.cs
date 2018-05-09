using Itgspelprojekt.Creatures;
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
    abstract class Battle
    {
        //variabler som örvs av subklasser
        protected animationForBattle battleAnimation, battleMenuAnimation, battleHealthbars;
        protected Texture2D battleTexture, menuBattle, healthMenuBattle;
        protected Player player;
        protected controlForUI mainBattleMenu;
        protected List<UI> UIList;
        protected float damage;

        public Battle(Texture2D background, Texture2D inventoryMenu, Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI, Player player)
        {
            //texture load
            battleTexture = background;
            menuBattle = inventoryMenu;
            healthMenuBattle = healthMenu;

            //laddar in player
            this.player = player;

            //laddar in andra klasser samt lisor
            UIList = listOfUI;
            mainBattleMenu = menyn;

            //battle animationer
            battleAnimation = new animationForBattle(battleTexture, new Vector2(0, 0), new Vector2(0, 0));
            battleMenuAnimation = new animationForBattle(menuBattle, new Vector2(1200, 1200), new Vector2(-1, -1));
            battleHealthbars = new animationForBattle(healthMenuBattle, new Vector2(1200, 0), new Vector2(-1, 60));
        }



    }
}



