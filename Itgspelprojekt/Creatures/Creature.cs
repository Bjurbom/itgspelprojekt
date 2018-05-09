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
    public class Creature
    {
        // Tommies och Tors Creature kod

        // Det här är en Creature i the Overworld. TODO: items, creatures in battle
        private bool move;
        public Vector2 position, direction, targetPosition; // targetPosition should only be used to move in a straight line.
        protected float moveSpeed;
        protected Texture2D texture;
        protected Rectangle hitboxUp, hitboxDown, hitboxLeft, hitboxRight; // will be set to texture.Bounds in the constructor
        private string name;
        public int sizeX = 64, sizeY = 64;
        protected bool goingUp, goingDown, goingLeft, goingRight;
        private float health;
        private Rectangle hitbox;
        protected List<Vector2> futureTargetPositions = new List<Vector2>();
        private float damage;
        public bool canDoBattle = true;

        // Tors properties
        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                return hitbox;
            }
            set
            {
                hitbox = value;
            }
        }

        public bool Move
        {
            get
            {
                return move;
            }
            set
            {
                move = value;
            }
        }



        public Creature(string name, Vector2 position, float moveSpeed, Texture2D texture)
        {
            this.name = name;
            this.position = position;
            this.targetPosition = new Vector2(position.X - position.X % 64, position.Y - position.Y % 64);
            this.moveSpeed = moveSpeed;
            this.texture = texture;
            move = true;
            health = 100;

            goingUp = true;
            goingDown = true;
            goingLeft = true;
            goingRight = true;
        }

        /// <summary>
        /// Feed a set of coordinates into the simple Creature AI, for the Creature to follow in order from index 0 to the end of the list.
        /// </summary>
        /// <param name="coordinates"></param>
        public void MoveTo(List<Vector2> coordinates)
        {
            futureTargetPositions = coordinates;
        }

        public void Update()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            
                if (targetPosition.X - moveSpeed / 2 - 1 > position.X) // moveSpeed is used so that when there's one 'tick' of motion left, it can teleport to it's destination, regardless of what the movement speed is set to
                    direction.X = 1f;
                else if (targetPosition.X + moveSpeed / 2 + 1 < position.X)
                    direction.X = -1f;
                else
                {
                    direction.X = 0;
                    position.X = targetPosition.X;

                if (move) // gör så att den stannar när den kommer intill fienden
                {

                    if (targetPosition.Y - moveSpeed / 2 - 1 > position.Y) // When X is correct, move on Y axis
                        direction.Y = 1f;
                    else if (targetPosition.Y + moveSpeed / 2 + 1 < position.Y)
                        direction.Y = -1f;
                    else
                    {
                        direction.Y = 0;
                        position.Y = targetPosition.Y;

                        if (futureTargetPositions.Count > 0)
                        {
                            targetPosition = futureTargetPositions[0];
                            futureTargetPositions.RemoveAt(0);
                        }
                    }
                }

                }
            



            position += direction * moveSpeed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawPosition = new Rectangle((int)position.X, (int)position.Y, sizeX, sizeY);
            spriteBatch.Draw(texture, drawPosition, Color.White);
        }
        public virtual void DrawInBattle(SpriteBatch spriteBatch, Rectangle drawPosition)
        {
            spriteBatch.Draw(texture, drawPosition, Color.White);
        }

    }
}
