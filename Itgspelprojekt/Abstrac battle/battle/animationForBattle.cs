﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.Abstrac_battle.battle
{
    //Tors animation för object
    class animationForBattle
    {
        private Texture2D sprite;
        private Vector2 endPosition;
        private Vector2 currentPosition;
        private bool inPosition;

        public animationForBattle(Texture2D sprite, Vector2 startPosition, Vector2 endPosition)
        {
            this.sprite = sprite;
            this.endPosition = endPosition;
            currentPosition = startPosition;
            inPosition = false;
        }

        public bool InPosition
        {
            get
            {
                return inPosition;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentPosition.X >= endPosition.X)
            {
                currentPosition.X += endPosition.X * 4;
            }
            if (currentPosition.Y >= endPosition.Y)
            {
                currentPosition.Y += endPosition.Y * 4;
            }

            if (currentPosition.Y <= endPosition.Y && currentPosition.X <= endPosition.X)
            {
                inPosition = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, currentPosition, Color.White);
        }


    }
}
