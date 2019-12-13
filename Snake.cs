using System;
using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Snake
    {
        public static Point snakeHeadPoint;
        public static List<Point> snakeBodyPoints = new List<Point>() { };
        static int bodyCount = 0; //temp
        static string lastMove;
        
        public static void GenerateSnakeHead()
        {
            Random random = new Random();
            snakeHeadPoint = new Point(random.Next(0, MapGenerator.sizeOfArray), random.Next(0, MapGenerator.sizeOfArray));
        }

        public static void MoveToObjective(Point objective)
        {
            Console.WriteLine(objective);
            ClearSnakeFromMap();
            if (snakeHeadPoint.X < objective.X)
            {
                snakeHeadPoint.X++;
                lastMove = "right";
            }
            else if (snakeHeadPoint.X > objective.X)
            {
                snakeHeadPoint.X--;
                lastMove = "left";
            }
            else if (snakeHeadPoint.Y < objective.Y)
            {
                snakeHeadPoint.Y++;
                lastMove = "up";
            }
            else if (snakeHeadPoint.Y > objective.Y)
            {
                snakeHeadPoint.Y--;
                lastMove = "down";
            }
            if (snakeHeadPoint == objective)
            {
                Eat(objective);
            }
            ConvertSnakeDataToMap();
        }

        static void ConvertSnakeDataToMap()
        {
            MapGenerator.map[snakeHeadPoint.X, snakeHeadPoint.Y] = MapTile.tileList[3];
            foreach (var point in snakeBodyPoints)
            {
                MapGenerator.map[point.X, point.Y] = MapTile.tileList[3];
            }
        }

        static void ClearSnakeFromMap()
        {
            MapGenerator.map[snakeHeadPoint.X, snakeHeadPoint.Y] = MapTile.tileList[1];
            foreach (var point in snakeBodyPoints)
            {
                MapGenerator.map[point.X, point.Y] = MapTile.tileList[1];
            }
        }

        static void Eat(Point foodPoint)
        {
            MapGenerator.map[foodPoint.X, foodPoint.Y] = MapTile.tileList[1];
            MapGenerator.foodPoints.Remove(foodPoint);
        }
    }
}