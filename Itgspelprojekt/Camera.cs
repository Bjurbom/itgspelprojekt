

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
        Matrix transform;
        Vector2 center;
        Viewport viewport;

        float zoom = 1;
        float rotation = 0;

        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }

        public float X
        {
            get
            {
                return center.X;
            }
            set
            {
                center.X = value;
            }
        }

        public float Y
        {
            get
            {
                return center.Y;
            }
            set
            {
                center.Y = value;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
                if (zoom < 0.1f)
                {
                    zoom = 0.1f;
                }
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 position)
        {
            center = new Vector2(position.X + 32, position.Y + 32);

            transform = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(Zoom, Zoom, 0) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }




       /* Vector2 position;
        Matrix viewMatrix;
        public int screenWidth, screenHeight;

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
        }*/
    }
}