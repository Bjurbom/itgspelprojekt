using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Creatures;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    static class Attack
    {
        //Tors simpla statiska attack på fiende

        /// <summary>
        /// gör en statisk klass som kan användas när en varelse anfaller en annan varalse
        /// bara en metod
        /// </summary>
        /// <param name="giver">den som anfaller</param>
        /// <param name="reciver">den som kommer ta skada</param>
        static public void AttackOnEnemy(Creature giver, Creature reciver)
        {
            reciver.Health -= giver.Damage;
        }


    }
}
