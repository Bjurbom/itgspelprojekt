using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Itgspelprojekt.XML;

namespace Itgspelprojekt.Menus
{
    class SettingsMenu
    {
        // Tommies magnificenta settingsmeny


        SpriteFont fontSmall;
        SpriteFont fontBig;

        Vector2 debugBtnPosition;
        bool debugActive = true;
        bool isDebugSelected = false;
        bool hasDebugBeenPressed = false;

        List<List<int>> resolutions = new List<List<int>>();
        Vector2 resolutionBtnPosition;
        int selectedResolution;
        int currentResolution;
        bool isResolutionSelected;
        bool hasResolutionBeenChanged = false;

        Vector2 applyBtnPosition;
        bool isApplySelected = false;

        Vector2 backBtnPosition;
        bool isBackSelected = false;


        public SettingsMenu(ContentManager contentManager)
        {
            fontSmall = contentManager.Load<SpriteFont>("Arial20");
            fontBig = contentManager.Load<SpriteFont>("Arial20Bold");
            resolutionBtnPosition = new Vector2(100, 100);
            debugBtnPosition = new Vector2(100, 200);
            applyBtnPosition = new Vector2(130, 350);
            backBtnPosition = new Vector2(230, 350);

            XMLReader xmlReader = new XMLReader();
            resolutions = xmlReader.ReadResolutions();
        }

        public Settings Update(KeyboardState ks, MouseState ms)
        {
            int mx = ms.Position.X; // for shortening the code a bit :)
            int my = ms.Position.Y;

            // calculate how large (in pixels) the buttons are
            Vector2 debugSize = fontSmall.MeasureString("Debug mode: " + (debugActive ? "yes" : "no"));
            Vector2 resolutionBtnSize = fontSmall.MeasureString("Resolution: " + resolutions[selectedResolution][0] + ", " + resolutions[selectedResolution][1]);
            Vector2 applyBtnSize = fontSmall.MeasureString("Apply");
            Vector2 backBtnSize = fontSmall.MeasureString("Back");

            // check if mouse is within the option
            isDebugSelected = mx > debugBtnPosition.X && mx < debugBtnPosition.X + debugSize.X && my > debugBtnPosition.Y && my < debugBtnPosition.Y + debugSize.Y;
            isResolutionSelected = mx > resolutionBtnPosition.X && mx < resolutionBtnPosition.X + resolutionBtnSize.X && my > resolutionBtnPosition.Y && my < resolutionBtnPosition.Y + resolutionBtnSize.Y;
            isApplySelected = mx > applyBtnPosition.X && mx < applyBtnPosition.X + applyBtnSize.X && my > applyBtnPosition.Y && my < applyBtnPosition.Y + applyBtnSize.Y;
            isBackSelected = mx > backBtnPosition.X && mx < backBtnPosition.X + backBtnSize.X && my > backBtnPosition.Y && my < backBtnPosition.Y + backBtnSize.Y;

            // check if resolution is being pressed/clicked
            if (isResolutionSelected & ks.IsKeyDown(Keys.D) | isResolutionSelected & ks.IsKeyDown(Keys.Right) | isResolutionSelected & ms.LeftButton == ButtonState.Pressed)
            {
                if (!hasResolutionBeenChanged)
                    selectedResolution++;
                hasResolutionBeenChanged = true;
            }
            else if (isResolutionSelected & ks.IsKeyDown(Keys.A) | isResolutionSelected & ks.IsKeyDown(Keys.Left))
            {
                if (!hasResolutionBeenChanged)
                    selectedResolution--;
                hasResolutionBeenChanged = true;
            }
            else
                hasResolutionBeenChanged = false;

            // check if debug is being pressed/clicked
            if (isDebugSelected & ks.IsKeyDown(Keys.Space) | isDebugSelected & ks.IsKeyDown(Keys.Enter) | isDebugSelected & ms.LeftButton == ButtonState.Pressed)
            {
                if (!hasDebugBeenPressed)
                    debugActive = !debugActive;
                hasDebugBeenPressed = true;
            }
            else
                hasDebugBeenPressed = false;

            // loop around to minimum/maximum resolution
            if (selectedResolution < 0)
                selectedResolution = resolutions.Count - 1;
            if (selectedResolution >= resolutions.Count)
                selectedResolution = 0;

            // check if Apply button is being pressed/clicked
            if (isApplySelected & ks.IsKeyDown(Keys.Space) | isApplySelected & ks.IsKeyDown(Keys.Enter) | isApplySelected & ms.LeftButton == ButtonState.Pressed)
                currentResolution = selectedResolution;

            // check if Back button is being pressed/clicked
            bool goBack = false;
            if (isBackSelected & ks.IsKeyDown(Keys.Space) | isBackSelected & ks.IsKeyDown(Keys.Enter) | isBackSelected & ms.LeftButton == ButtonState.Pressed)
                goBack = true;

            // finish up
            Settings settings = new Settings
            {
                debugActive = debugActive,
                resolution = resolutions[currentResolution].ToArray(),
                goBack = goBack
            };

            return settings;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(isDebugSelected ? fontBig : fontSmall, "Debug mode: " + (debugActive ? "yes" : "no"), debugBtnPosition, Color.Black);
            spriteBatch.DrawString(isResolutionSelected ? fontBig : fontSmall, "Resolution: " + resolutions[selectedResolution][0] + ", " + resolutions[selectedResolution][1], resolutionBtnPosition, Color.Black);
            spriteBatch.DrawString(isApplySelected ? fontBig : fontSmall, "Apply", applyBtnPosition, Color.Black);
            spriteBatch.DrawString(isBackSelected ? fontBig : fontSmall, "Back", backBtnPosition, Color.Black);
        }
    }
}
