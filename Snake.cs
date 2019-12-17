using System;
using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Snake
    {
        public static List<Point> snakeBodyPoints = new List<Point>() { };
        public static bool followSnakeHead = false;
        public static Point closestFood = new Point();
        static Point snakeTailPoint = new Point();
        public static bool outputObjective = false;
        
        public static void GenerateSnakeHead()
        {
            Random random = new Random();
            snakeBodyPoints.Add(new Point(random.Next(0, MapGenerator.sizeOfArray), random.Next(0, MapGenerator.sizeOfArray)));
        }

        public static void MoveSnake(Point objective)
        {
            ClearSnakeTailFromMap();
            snakeTailPoint = snakeBodyPoints[snakeBodyPoints.Count - 1];
            for (int i = snakeBodyPoints.Count - 1; i > 0; i--)
            {
                snakeBodyPoints[i] = snakeBodyPoints[i - 1];
            }
            PathFind(objective);
            //PathFind2();
            CollisionCheck();
            TryToEat();
            ConvertSnakeHeadToMap();
        }

        static void PathFind(Point objective)
        {
            if (snakeBodyPoints[0].X < objective.X)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X + 1, snakeBodyPoints[0].Y);
            }
            else if (snakeBodyPoints[0].X > objective.X)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X - 1, snakeBodyPoints[0].Y);
            }
            else if (snakeBodyPoints[0].Y < objective.Y)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
            }
            else if (snakeBodyPoints[0].Y > objective.Y)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y - 1);
            }
        }

        static bool hitBottom = false;
        static bool moveRight = false;
        static bool moveLeft = false;

        static void PathFind2()
        {
            if (moveLeft == false && moveRight == false && hitBottom == false && snakeBodyPoints[0].Y < MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
                if (snakeBodyPoints[0].Y == MapGenerator.sizeOfArray - 2)
                {
                    hitBottom = true;
                    moveRight = true;
                }
            }
            else if (moveLeft == false && moveRight == false && hitBottom == true && snakeBodyPoints[0].Y > 1)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y - 1);
                if (snakeBodyPoints[0].Y == 1)
                {
                    hitBottom = false;
                    moveRight = true;
                }
            }
            else if (moveLeft == false && moveRight == true && snakeBodyPoints[0].X < MapGenerator.sizeOfArray - 1)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X + 1, snakeBodyPoints[0].Y);
                moveRight = false;
            }
            else if (moveLeft == true && snakeBodyPoints[0].X > 0)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X - 1, snakeBodyPoints[0].Y);
            }
            else if (snakeBodyPoints[0].Y == 0)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
                moveLeft = false;
            }
            else if (snakeBodyPoints[0].Y == MapGenerator.sizeOfArray - 1)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y - 1);
                moveLeft = false;
            }
            else if (snakeBodyPoints[0].Y == 1)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y - 1);
                moveLeft = true;
                moveRight = false;
            }
            else if (snakeBodyPoints[0].Y == MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
                moveLeft = true;
                moveRight = false;
            }
        }

        static void ConvertSnakeHeadToMap()
        {
            MapGenerator.map[snakeBodyPoints[0].X, snakeBodyPoints[0].Y] = MapTile.tileList[3];
        }

        static void ClearSnakeTailFromMap()
        {
            MapGenerator.map[snakeBodyPoints[snakeBodyPoints.Count - 1].X, snakeBodyPoints[snakeBodyPoints.Count - 1].Y] = MapTile.tileList[1];
        }

        static void TryToEat()
        {
            for (int i = 0; i < MapGenerator.foodPoints.Count; i++)
            {
                if (MapGenerator.foodPoints[i] == snakeBodyPoints[0])
                {
                    MapGenerator.map[MapGenerator.foodPoints[i].X, MapGenerator.foodPoints[i].Y] = MapTile.tileList[3];
                    MapGenerator.foodPoints.Remove(MapGenerator.foodPoints[i]);
                    if (MapGenerator.foodPoints.Count == 0)
                    {
                        Console.WriteLine("Out of food!");
                        FormControls.timerEnabled = false;
                    }
                    snakeBodyPoints.Add(snakeTailPoint);
                    closestFood = FindClosestsFood();
                    break;
                }
            }
        }

        public static void FollowSnakeHead()
        {
            if (snakeBodyPoints[0].X >= 5 && snakeBodyPoints[0].X <= MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal)
            {
                FormControls.startingPointX = snakeBodyPoints[0].X - 5;
            }
            else if (snakeBodyPoints[0].X < 5)
            {
                FormControls.startingPointX = 0;
            }
            else if (snakeBodyPoints[0].X > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal)
            {
                FormControls.startingPointX = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal;
            }
            if (snakeBodyPoints[0].Y >= 5 && snakeBodyPoints[0].Y <= MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical)
            {
                FormControls.startingPointY = snakeBodyPoints[0].Y - 5;
            }
            else if (snakeBodyPoints[0].Y < 5)
            {
                FormControls.startingPointY = 0;
            }
            else if (snakeBodyPoints[0].Y > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical)
            {
                FormControls.startingPointY = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical;
            }
        }

        public static Point FindClosestsFood()
        {
            double shortestDistance = double.MaxValue;
            Point closestFoodPoint = new Point();
            foreach (var food in MapGenerator.foodPoints)
            {
                if (Math.Sqrt(Math.Pow(food.X - snakeBodyPoints[0].X, 2) + Math.Pow(food.Y - snakeBodyPoints[0].Y, 2)) < shortestDistance)
                {
                    shortestDistance = Math.Sqrt(Math.Pow(food.X - snakeBodyPoints[0].X, 2) + Math.Pow(food.Y - snakeBodyPoints[0].Y, 2));
                    closestFoodPoint = food;
                }
            }
            return closestFoodPoint;
        }

        static void CollisionCheck()
        {
            for (int i = 1; i < snakeBodyPoints.Count; i++)
            {
                if (snakeBodyPoints[0] == snakeBodyPoints[i])
                {
                    Console.WriteLine("Ouch! Don't bite yourself.");
                }
            }
        }
    }
}