﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Itgspelprojekt.Creatures
{
    abstract class Creature
    {
        // Det här är en Creature i the Overworld. TODO: items, creatures in battle



        public Vector2 position, direction, targetPosition; // targetPosition should only be used to move in a straight line.
        public float moveSpeed;
        public Texture2D texture;
        public Rectangle hitbox; // will be set to texture.Bounds in the constructor
        public string name;
        public int sizeX = 64, sizeY = 64;

        public Creature (string name, Vector2 position, float moveSpeed, Texture2D texture)
        {
            this.name = name;
            this.position = position;
            this.targetPosition = position;
            this.moveSpeed = moveSpeed;
            this.texture = texture;
            this.hitbox = texture.Bounds;
        }
        
        public void Update()
        {
            if (targetPosition.X - 4 > position.X) // player only has to move 90% of the distance, so will only move at 90% of moveSpeed
                direction.X = 1f;
            else if (targetPosition.X + 4 < position.X)
                direction.X = -1f;
            else
            {
                direction.X = 0;
                position.X = targetPosition.X;

                if (targetPosition.Y - 4 > position.Y) // When X is correct, move on Y axis
                    direction.Y = 1f;
                else if (targetPosition.Y + 4 < position.Y)
                    direction.Y = -1f;
                else
                {
                    direction.Y = 0;
                    position.Y = targetPosition.Y;
                }
            }
            
            position += direction * moveSpeed;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawPosition = new Rectangle((int)position.X, (int)position.Y, sizeX, sizeY);
            spriteBatch.Draw(texture, drawPosition, Color.White);
        }
        
    }
}
