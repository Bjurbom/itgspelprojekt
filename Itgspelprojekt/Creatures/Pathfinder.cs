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

        public List<Vector2> PathFind()
        {
            Vector2 startPosition = new Vector2(777, 1024);
            Vector2 endPosition = new Vector2(2560, 100);
            int startPosX = (int)((startPosition.X + 1) / 64); // Transform coordinates so that one tile is 1x1 large.
            int startPosY = (int)((startPosition.Y + 1) / 64); // + 1 to prevent rounding errors, not sure if necessary
            int endPosX = (int)((endPosition.X + 1) / 64);
            int endPosY = (int)((endPosition.Y + 1) / 64);


            List<List<Node>> node = new List<List<Node>>(); // GetLength(0) is height, 1 is width

            for (int i = 0; i < level.map.GetLength(1); i++) // i = x, j = y
            {
                node.Add(new List<Node>());
                for (int j = 0; j < level.map.GetLength(0); j++)
                {
                    Vector2 distance = new Vector2(i, j) - endPosition;
                    node[i].Add(new Node(level.map[j, i], (float)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y)));
                    if (node[i][j].tileTypeID == 1)
                        node[i][j].isOpen = false;
                    node[i][j].distanceTraversed = int.MaxValue;
                }
            }
            node[startPosX][startPosY].distanceTraversed = 0;


            int loopCounter = 0;
            while (true) // Note - Do not ever require the path to traverse the edge of the map.
            {           // Make sure that a path is possible. If it's not, this while loop will never end. Fix that, maybe?
                loopCounter++;
                for (int i = 1; i < level.map.GetLength(1) - 1; i++) // Do not do the calculations for the edges of the map, to prevent out-of-range exceptions.
                {
                    for (int j = 1; j < level.map.GetLength(0) - 1; j++)
                    {
                        // check all surrounding nodes. get the lowest length ones coords as precedingNode, and traversedDistance = precedingdistance + 1;
                        if (node[i][j].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                            node[i][j].distanceTraversed <= node[i - 1][j].distanceTraversed &&
                            node[i][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                            node[i][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        { } // do nothing

                        else if (!node[i][j].isOpen) // since I don't remember the evaluation priority of | and || vs & and &&
                        { }

                        else if (node[i - 1][j].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                                 node[i - 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                                 node[i - 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i - 1][j].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i - 1;
                            node[i][j].precedingNodeY = j;
                        }

                        else if (node[i + 1][j].distanceTraversed <= node[i - 1][j].distanceTraversed &&
                                 node[i + 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                                 node[i + 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i + 1][j].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i + 1;
                            node[i][j].precedingNodeY = j;
                        }

                        else if (node[i][j - 1].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                                 node[i][j - 1].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                                 node[i][j - 1].distanceTraversed <= node[i - 1][j].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i][j - 1].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i;
                            node[i][j].precedingNodeY = j - 1;
                        }

                        else if (node[i][j + 1].distanceTraversed <= node[i][j - 1].distanceTraversed &&
                                 node[i][j + 1].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                                 node[i][j + 1].distanceTraversed <= node[i - 1][j].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i][j + 1].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i;
                            node[i][j].precedingNodeY = j + 1;
                        }


                        if (node[i][j].distanceTraversed < 0) // important because of integer overflow
                            node[i][j].distanceTraversed = int.MaxValue;
                    }
                }

                if (node[endPosX][endPosY].distanceTraversed != int.MaxValue) // when the end position has been reached.
                {
                    break;
                }
                if (loopCounter >= 1000)
                {
                    throw new Exception("got stuck in a (presumably) never-ending loop.");
                }
            }


            // convert into an array or list of coordinates.
            List<Vector2> coordinates = new List<Vector2>();
            int lastX = endPosX;
            int lastY = endPosY;
            coordinates.Add(new Vector2(lastX * 64, lastY * 64));
            loopCounter = 0;

            do // this can become an infinite loop if there's a bug
            {
                loopCounter++;
                int temporaryXValue = node[lastX][lastY].precedingNodeX;
                lastY = node[lastX][lastY].precedingNodeY;
                lastX = temporaryXValue;
                coordinates.Add(new Vector2(lastX * 64, lastY * 64));
                if (loopCounter >= 1000)
                {
                    throw new Exception("got stuck in a (presumably) never-ending loop.");
                }
            }
            while (coordinates[coordinates.Count - 1] != new Vector2(startPosX * 64, startPosY * 64));

            coordinates.Reverse();
            return coordinates;
        }

    }

    class Node
    {
        public int precedingNodeX, precedingNodeY;
        public float distanceLeft; // using the pythagorean theorem, tile = 64x64
        public int distanceTraversed; // in amount of nodes
        public bool isOpen = true; // if false, node is a dead end
        public int tileTypeID;

        public Node(int tileTypeID, float distanceLeft)
        {
            this.tileTypeID = tileTypeID;
            this.distanceLeft = distanceLeft;
        }

    }
}
