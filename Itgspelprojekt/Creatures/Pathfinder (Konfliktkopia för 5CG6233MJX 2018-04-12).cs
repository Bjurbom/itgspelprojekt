using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Itgspelprojekt.Tiles;

namespace Itgspelprojekt.Creatures
{
    class Pathfinder
    {
        // Tommies fina A* Pathfinding kod
        // return an array of coordinates for the "simple" player AI (in Player.cs) to follow.

        public void PathFind(Map map)
        {
            Vector2 startPosition = Vector2.Zero;
            Vector2 endPosition = Vector2.Zero;
            
            List<List<Node>> node = new List<List<Node>>();

            for (int i = 0; i < level.map.GetLength(0); i++)
            {
                level.map.GetLength(1);
            }

            while (true)
            {/*
                for (int i = 0; i < map.CollisionTiles.Count; i++)
                {
                    node.Add(new Node(map.CollisionTiles[i].Rectangle.Location.ToVector2(), map.CollisionTiles[i].Id, 1));
                }

                foreach (Node n in node)
                {
                    if (n.ID == 1)
                        n.isOpen = false;
                    Vector2 tmp = n.location - endPosition;
                    n.distanceLeft = (float)Math.Sqrt(tmp.X * tmp.X + tmp.Y * tmp.Y); // Pythagorean Theorem
                }


                */
                break;
            }

        }

    }

    class Node
    {
        public Vector2 location;
        public int precedingNode;
        public float distanceLeft; // using the pythagorean theorem
        public int distanceTraversed; // in amount of nodes
        public bool isOpen; // if false, node is a dead end
        public int ID;
        public int posX, posY;

        public Node(Vector2 location, int ID, int distanceLeft)
        {
            this.location = location;
            this.ID = ID;
            this.distanceLeft = distanceLeft;
        }

    }
}
