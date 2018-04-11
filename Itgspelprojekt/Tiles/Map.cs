
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Itgspelprojekt.Tiles
{
    class Map
    {
        //Tors Map kod

        //gör listan
        List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        

        //property av listan
        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

       

        //konstruktor || för senare?
        public Map() { }

        public void Generate(int[,] map, int size)
        {

            //tar varje block i 2d arrayen
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    //kollar vad det är för nummer
                    int number = map[y, x];

                    //vad som händer med numbret
                    if (number > 0)
                    {
                        //lägger i en tile i listan med nummer med storlek på det
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    }

                }
            }   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //ritar ut varje tile
            foreach(CollisionTiles tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

    }
}
