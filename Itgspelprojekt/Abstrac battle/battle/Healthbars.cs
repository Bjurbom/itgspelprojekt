using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Itgspelprojekt.Abstrac_battle.battle;
using Itgspelprojekt.Creatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    class Healthbars
    {
        //Tors kod

        Rectangle healthbar;
        float health;
        private float healthDiffrents;
        Vector2 position;
        Color color;
        Texture2D texture;

        public Healthbars(Creature creature, Vector2 position, Color color)
        {
            // sätter in variablerna
            health = creature.Health;
            this.position = position;
            this.color = color;
            
            // gör så att man har alltid samma storlek på health oavsett hur mycket health fienden eller playern har
            healthDiffrents = 200f / health;

        }

        public void Update(Creature creature)
        {
            
            float temp;

            //tar healthen och får den att ha samma storlek
            temp = healthDiffrents * creature.Health;

            
            healthbar = new Rectangle((int)position.X, (int)position.Y, (int)temp, 20);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //ritar ut healthbaren
            spriteBatch.DrawRectangle(healthbar, color, 30f);
        }
    }
}
