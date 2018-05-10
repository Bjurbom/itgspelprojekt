using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Abstrac_battle.battle;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework.Graphics;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    class Healthbars : NormalBattle
    {
        public Healthbars(Texture2D background, Texture2D inventoryMenu, Texture2D healthMenu, controlForUI menyn, List<UI> listOfUI, Player player, SpriteFont spriteFont, SpriteBatch spriteBatch) : base(background, inventoryMenu, healthMenu, menyn, listOfUI, player, spriteFont, spriteBatch)
        {
        }
    }
}
