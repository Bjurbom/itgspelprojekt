
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
        public int screenWidth = 1280, screenHeight = 700;

        public Camera (int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
        }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }


        public void Update(Vector2 playerPosition)
        {
            // sätter ut mittpunkten på spelaren
            position.X = playerPosition.X - (screenWidth / 2);
            position.Y = playerPosition.Y - (screenHeight / 2);

            // där magi händer
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}