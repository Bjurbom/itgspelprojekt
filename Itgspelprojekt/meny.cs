using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt
{
        class Meny //kalle

        {
            MouseState mouseState;

            Texture2D menytexture;
            Vector2 position;
            public List<Meny_item> items = new List<Meny_item>();

            public Meny(ContentManager bilder)
            {
                //laddar in alla bilder och sätter positionen 
                menytexture = bilder.Load<Texture2D>("ITGGO paint");
                position = new Vector2(0, 0);

                Texture2D startgame = bilder.Load<Texture2D>("zstart game");
                Texture2D stargamechosen = bilder.Load<Texture2D>("zstart game chosen");
                Vector2 startgamepostion = new Vector2(850, 50);
                Meny_item startitem = new Meny_item(startgame, stargamechosen, startgamepostion, Gamestate.ingame);

                Texture2D instälningar = bilder.Load<Texture2D>("zsettings");
                Texture2D settingaschosen = bilder.Load<Texture2D>("zsettings chosen");
                Vector2 settingsposition = new Vector2(850, 150);
                Meny_item settingitem = new Meny_item(instälningar, settingaschosen, settingsposition, Gamestate.settings);

                Texture2D exitSprite = bilder.Load<Texture2D>("zexit");
                Texture2D exit = bilder.Load<Texture2D>("zexit chosen");
                Vector2 exitposition = new Vector2(850, 250);
                Meny_item exititem = new Meny_item(exitSprite, exit, exitposition, Gamestate.exit);
                //lägger till meny items bilderna i en listan "list" som sedan kan ritas ut 
                items.Add(startitem);
                items.Add(settingitem);
                items.Add(exititem);

            }

            public void update(GameTime gametime)
            {
                mouseState = Mouse.GetState();
                foreach (Meny_item item in items)
                {
                    if (item.Hitbox.Intersects(item.Mhitbox)) // tittar om muspekaren och items texturerna intersektar 
                {
                        item.chosen = true;

                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            Game1.gamestate = item.state;
                        }
                    }
                    else
                    {
                        item.chosen = false;
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(menytexture, position, Color.White);
                foreach (Meny_item items in items)
                {
                    items.Draw(spriteBatch);
                }
            }
        }
    }
