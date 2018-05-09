using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework.Graphics;

namespace Itgspelprojekt.battle.Enemy
{
    class EnemysTurn : NormalBattle
    {
        public EnemysTurn(Texture2D background, Texture2D inventoryMenu, Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI, Player player, SpriteFont spriteFont, SpriteBatch spriteBatch) : base(background, inventoryMenu, healthMenu, menyn, listOfUI, player, spriteFont, spriteBatch)
        {

        }
    }
}
