
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Itgspelprojekt
{
    class Camera
    {
        Vector2 position;
        Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public int ScreenWidth
        {
            get { return 1280; }
        }

        public int ScreenHeight
        {
            get { return 700; }
        }

        public void Update(Vector2 playerPosistion)
        {
            // sätter ut mmittpunkten på spelaren
            position.X = playerPosistion.X - (ScreenWidth / 2);
            position.Y = playerPosistion.Y - (ScreenHeight / 2);

            // där magi händer
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}