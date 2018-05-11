using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Itgspelprojekt.Map_generator;

namespace Itgspelprojekt.Creatures
{
    class Pathfinder
    {
        // Tommies fina pathfinding kod

        List<List<Node>> node = new List<List<Node>>(); // public so the distance to each tile can be written on the map, for debugging and because it looks interesting :)

        List<int> distancesX = new List<int>(); // these lists are used by A*
        List<int> distancesY = new List<int>();
        List<double> distancesDistance = new List<double>();

        public string errorMessage = String.Empty;


        /// <summary>
        /// Returns a list of cordinates for the simple player AI to follow.
        /// Pathfinding Type 0 = A*; Type 1 = CalRoutes; Type 2 = CalDiamond
        /// </summary>
        /// <param name="startingPosition"></param>
        /// <param name="endPosition"></param>
        /// <param name="PathfindingType"></param>
        /// <returns></returns>
        public List<Vector2> Pathfind(Vector2 startingPosition, Vector2 endPosition, int PathfindingType)
        {
            try
            {
                if (PathfindingType == 0)
                    return AStar(startingPosition, endPosition);
                else if (PathfindingType == 1)
                    return OldPathfinding(startingPosition, endPosition, false);
                else if (PathfindingType == 2)
                    return OldPathfinding(startingPosition, endPosition, true);
                else
                    throw new Exception("Invalid PathfindingType value. Must be 0, 1, or 2.");
            }
            catch (Exception)
            {
                errorMessage += "Pathfinding failed from " + startingPosition.ToString() + " to " + endPosition.ToString() + Environment.NewLine;
                return new List<Vector2>();
            }
        }


        public List<Vector2> AStar(Vector2 startingPosition, Vector2 endPosition)
        {
            int startPosX = (int)((startingPosition.X + 1) / 64); // Convert coordinates so that one tile is 1x1 large.
            int startPosY = (int)((startingPosition.Y + 1) / 64); // + 1 to prevent rounding errors, not sure if necessary
            int endPosX = (int)((endPosition.X + 1) / 64);
            int endPosY = (int)((endPosition.Y + 1) / 64);
            node = new List<List<Node>>();

            for (int i = 0; i < level.map.GetLength(1); i++) // i = x, j = y
            {
                node.Add(new List<Node>());
                for (int j = 0; j < level.map.GetLength(0); j++)
                {
                    node[i].Add(new Node());
                    node[i][j].distance = 1000000000;
                }
            }

            node[startPosX][startPosY].distanceTraversed = 0; // Initial values
            node[startPosX][startPosY].calculated = true;
            node[startPosX][startPosY].isOpen = false;
            int activeX = startPosX;
            int activeY = startPosY;

            if (activeX < 1 | activeX >= level.map.GetLength(1) | activeY < 1 | activeY >= level.map.GetLength(0))
            {
                throw new Exception("lol y u make start pos so near the edge of the map bro. Don't make start pos on map edge bRo.");
            }

            int loopCounter = 0; // This counter is used to throw exceptions when the game gets stuck in endless loops.

            distancesX = new List<int>(); // Reset lists
            distancesY = new List<int>();
            distancesDistance = new List<double>();

            distancesX.Add(activeX); // Add initial values to the lists
            distancesY.Add(activeY);
            distancesDistance.Add(node[activeX][activeY].distance);

            do
            {
                loopCounter++;

                CalculateAStarNode(activeX - 1, activeY, endPosX, endPosY); // Calculate the values of all surrounding nodes.
                CalculateAStarNode(activeX + 1, activeY, endPosX, endPosY);
                CalculateAStarNode(activeX, activeY - 1, endPosX, endPosY);
                CalculateAStarNode(activeX, activeY + 1, endPosX, endPosY);

                distancesX.RemoveAt(distancesX.Count - 1); // Remove the active node from distances lists
                distancesY.RemoveAt(distancesY.Count - 1);
                distancesDistance.RemoveAt(distancesDistance.Count - 1);
                

                if (node[activeX - 1][activeY].isOpen == true) // If it hasn't previously been in the distances lists, add it to the lists at the appropriate place
                {
                    distancesDistance.Add(node[activeX - 1][activeY].distance);
                    distancesX.Add(activeX - 1);
                    distancesY.Add(activeY);
                    SortDistances();
                }

                if (node[activeX + 1][activeY].isOpen == true) // If it hasn't previously been in the distances lists, add it to the lists at the appropriate place
                {
                    distancesDistance.Add(node[activeX + 1][activeY].distance);
                    distancesX.Add(activeX + 1);
                    distancesY.Add(activeY);
                    SortDistances();
                }

                if (node[activeX][activeY - 1].isOpen == true) // If it hasn't previously been in the distances lists, add it to the lists at the appropriate place
                {
                    distancesDistance.Add(node[activeX][activeY - 1].distance);
                    distancesX.Add(activeX);
                    distancesY.Add(activeY - 1);
                    SortDistances();
                }

                if (node[activeX][activeY + 1].isOpen == true) // If it hasn't previously been in the distances lists, add it to the lists at the appropriate place
                {
                    distancesDistance.Add(node[activeX - 1][activeY + 1].distance);
                    distancesX.Add(activeX);
                    distancesY.Add(activeY + 1);
                    SortDistances();
                }


                node[activeX - 1][activeY].isOpen = false; // Makes sure that they're not added to the lists again.
                node[activeX + 1][activeY].isOpen = false;
                node[activeX][activeY - 1].isOpen = false;
                node[activeX][activeY + 1].isOpen = false;

                activeX = distancesX[distancesX.Count - 1]; // Sets activeX & activeY to the calculated node that has the lowest distance value.
                activeY = distancesY[distancesY.Count - 1];


                if (loopCounter >= 3000)
                {
                    throw new Exception("got stuck in a (presumably) never-ending loop.");
                }

            }
            while (node[endPosX][endPosY].distanceTraversed >= 100000000);
            

            List<Vector2> coordinates = new List<Vector2>();
            int lastX = endPosX;
            int lastY = endPosY;
            coordinates.Add(new Vector2(lastX * 64, lastY * 64)); // multiply by 64 to convert back to the same coordinates used otherwise in the game
            loopCounter = 0;
            do
            {
                loopCounter++;
                activeX = lastX;
                activeY = lastY;

                double distance = node[activeX - 1][activeY].distanceTraversed; // Sets last node to the one to the left
                lastX = activeX - 1;
                lastY = activeY;

                if (distance > node[activeX + 1][activeY].distanceTraversed) // if node to the right has lower distanceTraversed, set it to last node
                {
                    distance = node[activeX + 1][activeY].distanceTraversed;
                    lastX = activeX + 1;
                    lastY = activeY;
                }

                if (distance > node[activeX][activeY - 1].distanceTraversed) // if node above has lower distanceTraversed, set it to last node
                {
                    distance = node[activeX][activeY - 1].distanceTraversed;
                    lastX = activeX;
                    lastY = activeY - 1;
                }

                if (distance > node[activeX][activeY + 1].distanceTraversed) // if node below has lower distanceTraversed, set it to last node
                {
                    distance = node[activeX][activeY + 1].distanceTraversed;
                    lastX = activeX;
                    lastY = activeY + 1;
                }

                coordinates.Add(new Vector2(lastX * 64, lastY * 64)); // Add last node to coordinates list.

                if (loopCounter >= 400)
                {
                    throw new Exception("got stuck in a (presumably) never-ending loop.");
                }
            }
            while (coordinates[coordinates.Count - 1] != new Vector2(startPosX * 64, startPosY * 64)); // while the start position has not been added to the list

            coordinates.Reverse();

            return coordinates;
        }

        // Sorts the distances lists, with lower distance values being closer to the end of the lists.
        private void SortDistances()
        {
            for (int i = distancesDistance.Count - 1; i > 0; i--) // sort the lists from end to beginning.
            {
                if (distancesDistance[i] > distancesDistance[i - 1])
                {
                    double d = distancesDistance[i - 1]; // Swap distancesLists[i] with distancesLists[i-1]
                    distancesDistance[i - 1] = distancesDistance[i];
                    distancesDistance[i] = d;
                    int x = distancesX[i - 1];
                    distancesX[i - 1] = distancesX[i];
                    distancesX[i] = x;
                    int y = distancesY[i - 1];
                    distancesY[i - 1] = distancesY[i];
                    distancesY[i] = y;
                }
                else
                    break;
            }
        }
        
        // This method calculates the value of a single node in the A* algorithm.
        public void CalculateAStarNode(int valueX, int valueY, int targetX, int targetY)
        {
            if (node[valueX][valueY].calculated) // if the node has already been calculated
            { }
            else if (level.map[valueY, valueX] <= 1) // if the node is a wall.
            {
                node[valueX][valueY].distance = double.MaxValue;
                node[valueX][valueY].calculated = true;
            }
            else
            {
                int precedingX, precedingY;
                node[valueX][valueY].distanceTraversed = node[valueX - 1][valueY].distanceTraversed + 1; // set the nodes values, based on the node to its left.
                precedingX = valueX - 1;
                precedingY = valueY;

                if (node[valueX + 1][valueY].distanceTraversed < node[valueX][valueY].distanceTraversed) // if the node to the right has lower distanceTraversed, ...
                {                                                                                        // ... then set this nodes values, based on the node to the right
                    node[valueX][valueY].distanceTraversed = node[valueX + 1][valueY].distanceTraversed + 1;
                    precedingX = valueX + 1;
                    precedingY = valueY;
                }

                if (node[valueX][valueY - 1].distanceTraversed < node[valueX][valueY].distanceTraversed) // if the node above has lower distanceTraversed, ...
                {                                                                                        // ... then set this nodes values, based on the node above
                    node[valueX][valueY].distanceTraversed = node[valueX][valueY - 1].distanceTraversed + 1;
                    precedingX = valueX;
                    precedingY = valueY - 1;
                }

                if (node[valueX][valueY + 1].distanceTraversed < node[valueX][valueY].distanceTraversed) // if the node below has lower distanceTraversed, ...
                {                                                                                        // ... then set this nodes values, based on the node below
                    node[valueX][valueY].distanceTraversed = node[valueX][valueY + 1].distanceTraversed + 1;
                    precedingX = valueX;
                    precedingY = valueY + 1;
                }

                node[valueX][valueY].distance = node[valueX][valueY].distanceTraversed + Math.Sqrt((targetX - valueX) * (targetX - valueX) + (targetY - valueY) * (targetY - valueY));
                                                                                        // This above here is the pythagorean theorem. IDK whether or not to use Math.Pow();
                node[valueX][valueY].calculated = true;
            }

        }


        public List<Vector2> OldPathfinding(Vector2 startingPosition, Vector2 endPosition, bool useCalDiamond)
        {
            int startPosX = (int)((startingPosition.X + 1) / 64); // Convert coordinates so that one tile is 1x1 large.
            int startPosY = (int)((startingPosition.Y + 1) / 64); // + 1 to prevent rounding errors, not sure if necessary
            int endPosX = (int)((endPosition.X + 1) / 64);
            int endPosY = (int)((endPosition.Y + 1) / 64);


            node = new List<List<Node>>(); // level.map.GetLength(0) is height, 1 is width

            for (int i = 0; i < level.map.GetLength(1); i++) // i = x, j = y
            {
                node.Add(new List<Node>());
                for (int j = 0; j < level.map.GetLength(0); j++)
                {
                    Vector2 distance = new Vector2(i, j) - endPosition;
                    node[i].Add(new Node(level.map[j, i]));
                    if (node[i][j].tileTypeID == 1)
                        node[i][j].isOpen = false;
                    node[i][j].distanceTraversed = int.MaxValue; // could be less, e.g half of MaxValue
                }
            }

            node[startPosX][startPosY].distanceTraversed = 0;

            if (useCalDiamond)
                CalculateDiamond(endPosX, endPosY, startPosX, startPosY);
            else
                CalculateRoutes(endPosX, endPosY);


            // convert into an array or list of coordinates.
            List<Vector2> coordinates = new List<Vector2>();
            int lastX = endPosX;
            int lastY = endPosY;
            coordinates.Add(new Vector2(lastX * 64, lastY * 64)); // multiply by 64 to convert back to the same coordinates used otherwise in the game
            int loopCounter = 0;
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

        /// <summary>
        /// Calculate the route in the shape of a diamond.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="endPosX"></param>
        /// <param name="endPosY"></param>
        /// <param name="startPosX"></param>
        /// <param name="startPosY"></param>
        /// <returns></returns>
        public void CalculateDiamond(int endPosX, int endPosY, int startPosX, int startPosY)
        {
            int loopCounter = 0;
            int iterationCounter = 0;
            int sign = -1;
            do
            {
                iterationCounter += 1;
                for (int i = sign * -iterationCounter; i != 34 + 34 * sign; i += sign) //This calculates all nodes that are (iterationCounter) tiles away from starting node
                {
                    CalculateNodeValue(startPosX + i - iterationCounter, startPosY + i);
                    CalculateNodeValue(startPosX + -i, startPosY + i - iterationCounter);
                    CalculateNodeValue(startPosX + iterationCounter - i, startPosY - i);
                    CalculateNodeValue(startPosX + i, startPosY + iterationCounter - i);
                }

                if (iterationCounter > level.map.GetLength(0) + level.map.GetLength(1))
                {
                    iterationCounter = 0;
                    sign = -sign; // if the diamond is at it's minimum size, make it grow. if it's at its maximum size, make it shrink.
                    loopCounter++;
                }

                if (loopCounter > 20)
                {
                    throw new Exception("got stuck in a (presumably) never-ending loop.");
                }
            }
            while (node[endPosX][endPosY].distanceTraversed == int.MaxValue);

        }


        public void CalculateRoutes(int endPosX, int endPosY)
        {
            int loopCounter = 0;
            while (true) // calculate the most efficient route to EVERY tile until a way to the end position has been found. Throw an exception if loopCounter >= 1000
            {            // very unoptimized loop
                         // Could be optimized by setting isOpen to false for nodes that have already had their value calculated.
                loopCounter++;
                for (int i = 1; i < level.map.GetLength(1) - 1; i++) // Calculate all nodes, left-to-right, then up-to-down. Do not calculate on edge of map, ...
                {                                                    // ... since that would cause exceptions to be thrown.
                    for (int j = 1; j < level.map.GetLength(0) - 1; j++)
                    {
                        CalculateNodeValue(i, j);
                    }
                }

                if (node[endPosX][endPosY].distanceTraversed != int.MaxValue) // when the end position has been reached by the pathfinding algorithm
                {
                    break;
                }

                for (int i = level.map.GetLength(1) - 1; i > 0; i--) // Calculate all nodes, right-to-left, then down-to-up. Do not calculate edge of map, bcuz exceptions.
                {
                    for (int j = level.map.GetLength(0) - 1; j > 0; j--)
                    {
                        CalculateNodeValue(i, j);
                    }
                }

                if (node[endPosX][endPosY].distanceTraversed != int.MaxValue) // when the end position has been reached by the pathfinding algorithm
                {
                    break;
                }

                if (loopCounter >= 200)
                {
                    throw new Exception("Got stuck in a (presumably) never-ending loop.");
                }
            }
        }

        // This method is used by the older pathfinding algorithms, CalRoutes and CalDiamond
        // It sets the nodes precedingNode values to the neighbouring node with the lowest distanceTraversed, ...
        // ... and sets the nodes own distanceTraversed to equal that nodes distanceTraversed + 1

        private void CalculateNodeValue(int i, int j)
        {
            if (i < 1 | i >= level.map.GetLength(1) - 1 | j < 1 | j >= level.map.GetLength(0) - 1) // prevents out-of-range exceptions. Unnecessary for CalRoutes, only CalDiamond
            { } // do nothing

            else if (!node[i][j].isOpen || // if node[i][j] is closed, i.e.not accessible
               node[i][j].distanceTraversed <= node[i + 1][j].distanceTraversed && // if node[i][j].distanceTraversed is lower than all of its neighbours' distanceTraversed
               node[i][j].distanceTraversed <= node[i - 1][j].distanceTraversed &&
               node[i][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
               node[i][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
            {
                if (node[i][j].distanceTraversed < 0) // Prevent/undo integer overflow
                    node[i][j].distanceTraversed = int.MaxValue;
            }

            else if (node[i - 1][j].distanceTraversed <= node[i + 1][j].distanceTraversed && // if node[i-1][j].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                     node[i - 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                     node[i - 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
            {
                node[i][j].distanceTraversed = node[i - 1][j].distanceTraversed + 1;
                node[i][j].precedingNodeX = i - 1;
                node[i][j].precedingNodeY = j;
                if (node[i][j].distanceTraversed < 0) // Prevent/undo integer overflow
                    node[i][j].distanceTraversed = int.MaxValue;
            }

            else if (node[i + 1][j].distanceTraversed <= node[i - 1][j].distanceTraversed && // if node[i+1][j].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                     node[i + 1][j].distanceTraversed <= node[i][j + 1].distanceTraversed &&
                     node[i + 1][j].distanceTraversed <= node[i][j - 1].distanceTraversed)
            {
                node[i][j].distanceTraversed = node[i + 1][j].distanceTraversed + 1;
                node[i][j].precedingNodeX = i + 1;
                node[i][j].precedingNodeY = j;
                if (node[i][j].distanceTraversed < 0) // Prevent/undo integer overflow
                    node[i][j].distanceTraversed = int.MaxValue;
            }

            else if (node[i][j - 1].distanceTraversed <= node[i][j + 1].distanceTraversed && // if node[i][j-1].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                     node[i][j - 1].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                     node[i][j - 1].distanceTraversed <= node[i - 1][j].distanceTraversed)
            {
                node[i][j].distanceTraversed = node[i][j - 1].distanceTraversed + 1;
                node[i][j].precedingNodeX = i;
                node[i][j].precedingNodeY = j - 1;
                if (node[i][j].distanceTraversed < 0) // Prevent/undo integer overflow
                    node[i][j].distanceTraversed = int.MaxValue;
            }

            else if (node[i][j + 1].distanceTraversed <= node[i][j - 1].distanceTraversed && // if node[i][j+1].distanceTraversed is lower than all other neighbours, and node[i][j] itself
                     node[i][j + 1].distanceTraversed <= node[i + 1][j].distanceTraversed &&
                     node[i][j + 1].distanceTraversed <= node[i - 1][j].distanceTraversed)
            {
                node[i][j].distanceTraversed = node[i][j + 1].distanceTraversed + 1;
                node[i][j].precedingNodeX = i;
                node[i][j].precedingNodeY = j + 1;
                if (node[i][j].distanceTraversed < 0) // Prevent/undo integer overflow
                    node[i][j].distanceTraversed = int.MaxValue;
            }
        }
    }

    class Node
    {
        public int precedingNodeX, precedingNodeY;
        public float distanceLeft; // using the pythagorean theorem. tile = 64x64. Currently unused.
        public int distanceTraversed = 2000000000; // in amount of nodes. tile = 1x1
        public double distance; // distanceLeft+distanceTraversed.
        public bool isOpen = true; // if false, node is a dead end
        public bool calculated = false; // Indicates whether or not the nodes values (precedingNode, distanceLeft, distanceTraversed) have already been calculated.
        public int tileTypeID; // what type of tile is on the map at this node

        public Node() // A*
        {

        }

        public Node(int tileTypeID) // CalRoutes, CalDiamond
        {
            this.tileTypeID = tileTypeID;
        }


    }
}
