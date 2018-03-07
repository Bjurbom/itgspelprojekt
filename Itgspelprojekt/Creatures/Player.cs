using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Itgspelprojekt.Creatures
{
    class Player : Creature
    {
        public Player(string name, Vector2 position, float moveSpeed, Texture2D texture) : base(name, position, moveSpeed, texture)
        {

        }

        public void PlayerUpdate()
        {
            // up
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                targetPosition.Y = position.Y + (float)64;
            }
            // down
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {

            }
            // right 
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {

            }
            // left
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

            }
        }
    }
}
