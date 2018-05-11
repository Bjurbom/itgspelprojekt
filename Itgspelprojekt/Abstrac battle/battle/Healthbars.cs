using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Abstrac_battle.battle;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    class Healthbars
    {
        //Tors kod

        Rectangle healthbar;
        float health;
        private float healthDiffrents;
        float temp;
        Vector2 position;

        public Healthbars(Creature creature, Vector2 position)
        {
            health = creature.Health;
            this.position = position;

            healthDiffrents = 200f / health;

        }

        public void Update(Creature creature)
        {
            float temp;
            temp = healthDiffrents * creature.Health;

            healthbar = new Rectangle((int)position.X, (int)position.Y, (int)temp, 100);

        }


    }
}
