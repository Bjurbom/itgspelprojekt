using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Itgspelprojekt.Map_generator;

namespace Itgspelprojekt.Creatures
{

    // Tommies och Tors player kod
    class Player : Creature
    {
        private List<Creature> creatures;

        public Player(string name, Vector2 position, float moveSpeed, Texture2D texture, List<Creature> creatures) : base(name, position, moveSpeed, texture)
        {
            this.creatures = creatures;

        }

        public void PlayerUpdate()
        {
            //skapar hitboxen åt spelarna
            hitboxUp = new Rectangle((int)position.X, (int)position.Y -60, 56, 56);
            hitboxDown = new Rectangle((int)position.X, (int)position.Y +60, 56, 56);
            hitboxLeft = new Rectangle((int)position.X -60, (int)position.Y, 56, 56);
            hitboxRight = new Rectangle((int)position.X +60, (int)position.Y, 56, 56);
            Hitbox = new Rectangle((int)position.X, (int)position.Y, 64, 64);

            

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                moveSpeed = 30;
            else
                moveSpeed = 6;

            if (position == targetPosition)
            {
                // up
                if (Keyboard.GetState().IsKeyDown(Keys.W) && goingUp)
                {
                    targetPosition.Y = position.Y - 64;
                }
                // down
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && goingDown)
                {
                    targetPosition.Y = position.Y + 64;
                }
                // right 
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && goingRight)
                {
                    targetPosition.X = position.X + 64;
                }
                // left
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && goingLeft)
                {
                    targetPosition.X = position.X - 64;
                }
            }

            
        }
        //snabb sätt att stänga av spelarens movements
        public void PlayerStop()
        {
            goingUp = false;
            goingRight = false;
            goingLeft = false;
            goingDown = false;
        }

        public void PlayerHitdetection(CollisionTiles item, Camera camera)
        {
            Collision(item, camera);
        }

        private void Collision(CollisionTiles item, Camera camera)
        {
            if (hitboxUp.Intersects(item.Rectangle))
            {
                if (item.Id == 1)
                {
                    goingUp = false;

                }
                else
                {
                    goingUp = true;
                }
            }


            if (hitboxDown.Intersects(item.Rectangle))
            {
                if (item.Id == 1)
                {
                    goingDown = false;
                }
                else
                {
                    goingDown = true;
                }
            }

            if (hitboxLeft.Intersects(item.Rectangle))
            {
                if (item.Id == 1)
                {
                    goingLeft = false;
                }
                else
                {
                    goingLeft = true;
                }
            }


            if (hitboxRight.Intersects(item.Rectangle))
            {
                if (item.Id == 1)
                {
                    goingRight = false;
                }
                else
                {
                    goingRight = true;
                }
            }
            if (Hitbox.Intersects(item.Rectangle))
            {
                if (item.Id == 3)
                {
                    // Zoom in effekt
                    camera.Zoom += 0.5f;
                    camera.Rotation += 0.5f;

                    //stoppar spelaren movement med en enkle metod
                    PlayerStop();

                    // 
                    if (camera.Zoom >= 30)
                    {

                        item.Id = 2;
                        Game1.gamestate = Gamestate.battle;
                        
                    }

                }
                

            }
        }
    }
}
