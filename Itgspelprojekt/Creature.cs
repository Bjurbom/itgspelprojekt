using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Itgspelprojekt
{
    enum Direction { Up, Down, Left, Right };
    abstract class Creature
    {
        // Det här är en Creature i the Overworld. TODO: items, creatures in battle



        protected Vector2 position, direction, targetPosition; // targetPosition should only be used to move in a straight line.
        protected float moveSpeed;
        protected Texture2D texture;
        protected Rectangle hitbox; // will be set to texture.Bounds in the constructor

        Creature (Vector2 position, float moveSpeed, Texture2D texture)
        {
            this.position = position;
            this.moveSpeed = moveSpeed;
            this.texture = texture;
            this.hitbox = texture.Bounds;
        }
        
        virtual public void Update()
        {
            if (targetPosition.X - 0.1 > position.X) // player only has to move 90% of the distance, so will only move at 90% of moveSpeed
                direction.X = 0.9f;
            else if (targetPosition.X + 0.1 < position.X)
                direction.X = -0.9f;
            else
            {
                direction.X = 0;
                position.X = targetPosition.X;

                if (targetPosition.Y - 0.1 > position.Y) // When X is correct, move on Y axis
                    direction.Y = 0.9f;
                else if (targetPosition.Y + 0.1 < position.Y)
                    direction.Y = -0.9f;
                else
                {
                    direction.Y = 0;
                    position.Y = targetPosition.Y;
                }
            }
            
            position += direction * moveSpeed;
        }
        

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
