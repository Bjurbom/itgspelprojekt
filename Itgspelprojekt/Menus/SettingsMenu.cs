using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Itgspelprojekt.Menus
{
    class SettingsMenu
    {
        // Tommies magnificenta settingsmeny

        Texture2D texture;
        Texture2D selectedTexture;
        SpriteFont fontSmall;
        SpriteFont fontBig;
        bool debugActive = true;
        bool debugSelected = false;
        Vector2 debugPosition;

        
        public SettingsMenu(ContentManager contentManager)
        {
            fontSmall = contentManager.Load<SpriteFont>("Arial20");
            fontBig = contentManager.Load<SpriteFont>("Arial20Bold");
            debugPosition = new Vector2(200, 300);
        }

        public Settings Update(MouseState ms)
        {
            int mx = ms.Position.X;
            int my = ms.Position.Y;
            Vector2 debugSize = fontSmall.MeasureString("Debug mode: " + (debugActive ? "yes" : "no"));
            
            if (mx > debugPosition.Y && mx < debugPosition.Y + debugSize.Y && my > debugPosition.Y && my < debugPosition.Y + debugSize.Y)
                debugSelected = true;
            else
                debugSelected = false;

            Vector2 resolution = Vector2.Zero;

            Settings settings = new Settings
            {
                debugActive = debugActive,
                resolution = resolution
            };

            return settings;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(fontBig, "abc")
            spriteBatch.DrawString(debugSelected ? fontBig : fontSmall, "Debug mode: " + (debugActive ? "yes" : "no"), new Vector2(200, 300), Color.Black);
        }
    }
}
