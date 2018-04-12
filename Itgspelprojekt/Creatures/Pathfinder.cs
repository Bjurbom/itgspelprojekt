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
        // Tommies fina pathfinding kod

        // return a list of coordinates for the "simple" player AI (in Player.cs) to follow.
        public List<Vector2> PathFind(Vector2 startingPosition, Vector2 endPosition)
        {
            int startPosX = (int)((startingPosition.X + 1) / 64); // Convert coordinates so that one tile is 1x1 large.
            int startPosY = (int)((startingPosition.Y + 1) / 64); // + 1 to prevent rounding errors, not sure if necessary
            int endPosX = (int)((endPosition.X + 1) / 64);
            int endPosY = (int)((endPosition.Y + 1) / 64);
            

            List<List<Node>> node = new List<List<Node>>(); // GetLength(0) is height, 1 is width

            for (int i = 0; i < level.map.GetLength(1); i++) // i = x, j = y
            {
                node.Add(new List<Node>());
                for (int j = 0; j < level.map.GetLength(0); j++)
                {
                    Vector2 distance = new Vector2(i, j) - endPosition;
                    node[i].Add(new Node(level.map[j, i], (float)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y))); // Pythagorean theorem
                    if (node[i][j].tileTypeID == 1)
                        node[i][j].isOpen = false;
                    node[i][j].distanceTraversed = int.MaxValue;
                }
            }
            node[startPosX][startPosY].distanceTraversed = 0;


            int loopCounter = 0;
            while (true) // calculate the most efficient route to EVERY tile until a way to the end position has been found. Throw an exception if loopCounter >= 1000
            {            // very unoptimized loop, will maybe try to improve it, e.g. using heuristics.
                loopCounter++;
                for (int i = 1; i < level.map.GetLength(1) - 1; i++) // Do not do the calculations for the edges of the map, to prevent out-of-range exceptions.
                {
                    for (int j = 1; j < level.map.GetLength(0) - 1; j++)
                    {

                        if (!node[i][j].isOpen || node[i][j].distanceTraversed <= node[i + 1][j].distanceTraversed && // if node[i][j] is closed, i.e. not accessible, or ...
                            node[i][j].distanceTraversed <= node[i - 1][j].distanceTraversed && // ... if node[i][j].distanceTraversed is lower than all of its neighbours' distanceTraversed
                            node[i][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                            node[i][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        { } // do nothing
                        

                        else if (node[i - 1][j].distanceTraversed <= node[i + 1][j].distanceTraversed && // if node[i-1][j].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                                 node[i - 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                                 node[i - 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i - 1][j].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i - 1;
                            node[i][j].precedingNodeY = j;
                        }

                        else if (node[i + 1][j].distanceTraversed <= node[i - 1][j].distanceTraversed && // if node[i+1][j].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                                 node[i + 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                                 node[i + 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i + 1][j].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i + 1;
                            node[i][j].precedingNodeY = j;
                        }

                        else if (node[i][j - 1].distanceTraversed <= node[i][j + 1].distanceTraversed && // if node[i][j-1].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                                 node[i][j - 1].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                                 node[i][j - 1].distanceTraversed <= node[i - 1][j].distanceTraversed)
                        {
                            node[i][j].distanceTraversed = node[i][j - 1].distanceTraversed + 1;
                            node[i][j].precedingNodeX = i;
                            node[i][j].precedingNodeY = j - 1;
                        }

                        else if (node[i][j + 1].distanceTraversed <= node[i][j - 1].distanceTraversed && // if node[i][j+1].distanceTraversed is lower than all other neighbours, and node[i][j] itself
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

                if (node[endPosX][endPosY].distanceTraversed != int.MaxValue) // when the end position has been reached by the pathfinding algorithm
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
            coordinates.Add(new Vector2(lastX * 64, lastY * 64)); // multiply by 64 to convert back to the same coordinates used otherwise in the game
            loopCounter = 0;

            do
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
            while (coordinates[coordinates.Count - 1] != new Vector2(startPosX * 64, startPosY * 64)); // while the start position has not been added to the list

            coordinates.Reverse(); // since it's in the wrong order :P
            return coordinates;
        }

    }

    class Node
    {
        public int precedingNodeX, precedingNodeY;
        public float distanceLeft; // using the pythagorean theorem. tile = 64x64. Currently unused.
        public int distanceTraversed; // in amount of nodes. tile = 1x1
        public bool isOpen = true; // if false, node is a dead end
        public int tileTypeID; // what type of tile is on the map at this node

        public Node(int tileTypeID, float distanceLeft)
        {
            this.tileTypeID = tileTypeID;
            this.distanceLeft = distanceLeft;
        }

    }
}
