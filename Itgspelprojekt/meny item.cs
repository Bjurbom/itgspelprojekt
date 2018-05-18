using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Itgspelprojekt 
{
        class Meny_item : Bas_klass //kalle
        {
            public bool chosen = false;
            public Gamestate state;


            public Meny_item(Texture2D texture, Texture2D textureChosen, Vector2 posision, Gamestate state)
            {
                this.textureChosen = textureChosen;
                this.texture = texture;
                this.position = posision;
                this.state = state;


            }
            public Rectangle Hitbox // hitboxen för texturerna som man  klickar på 
            {
                get
                {
                    Rectangle hitbox = new Rectangle();
                    hitbox.Location = position.ToPoint();

                    hitbox.Width = texture.Width;
                    hitbox.Height = texture.Height;

                    return hitbox;
                }
            }

            public Rectangle Mhitbox // hitboxen för muspekaren så att den vet när den är på items textuterna 
            {
                get
                {
                    Rectangle mhitbox = new Rectangle();
                    mhitbox.Location = Mouse.GetState().Position;
                    mhitbox.Width = 1;
                    mhitbox.Height = 1;
                    return mhitbox;

                }
            }




            public void Draw(SpriteBatch spriteBatch)
            {

                if (chosen == true)
                {
                    spriteBatch.Draw(textureChosen, position, Color.White);
                }
                else
                    spriteBatch.Draw(texture, position, Color.White);
            }

        }
    }

