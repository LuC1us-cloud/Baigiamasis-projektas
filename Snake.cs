using System;
using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Snake
    {
        public static List<Point>[] snakeBodyPoints = new List<Point>[MapGenerator.AmountOfMaps];
        public static bool followSnakeHead = false;
        public static Point[] closestFood = new Point[MapGenerator.AmountOfMaps];
        static Point[] snakeTailPoint = new Point[MapGenerator.AmountOfMaps];
        public static bool outputObjective = false;
        public static bool[] canMove = new bool[MapGenerator.AmountOfMaps];
        
        public static void GenerateSnakeHead(int index)
        {
            Random random = new Random();
            snakeBodyPoints[index].Add(new Point(random.Next(0, MapGenerator.sizeOfArray), random.Next(0, MapGenerator.sizeOfArray)));
            hitBottom[index] = false;
            moveRight[index] = false;
            moveLeft[index] = false;
            canMove[index] = true;
        }

        public static void MoveSnake(Point objective, int index)
        {
                ClearSnakeTailFromMap(index);
                snakeTailPoint[index] = snakeBodyPoints[index][snakeBodyPoints[index].Count - 1];
                for (int y = snakeBodyPoints[index].Count - 1; y > 0; y--)
                {
                    snakeBodyPoints[index][y] = snakeBodyPoints[index][y - 1];
                }
                if (index == 0)
                {
                    PathFind(objective, index);
                }
                else
                {
                    PathFind2(index);
                }
                //PathFind3(objective, i);
                TryToEat(index);
                ConvertSnakeHeadToMap(index); 
        }

        /// <summary>
        /// Moves to closes food, goes over itself it is needed.
        /// </summary>
        /// <param name="objective"></param>
        static void PathFind(Point objective, int index)
        {
            if (snakeBodyPoints[index][0].X < objective.X)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].X > objective.X)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X - 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].Y < objective.Y)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
            }
            else if (snakeBodyPoints[index][0].Y > objective.Y)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
            }
        }

        static bool[] hitBottom = new bool[MapGenerator.AmountOfMaps];
        static bool[] moveRight = new bool[MapGenerator.AmountOfMaps];
        static bool[] moveLeft = new bool[MapGenerator.AmountOfMaps];

        /// <summary>
        /// Sweeps map for food
        /// </summary>
        static void PathFind2(int index)
        {
            if (moveLeft[index] == false && moveRight[index] == false && hitBottom[index] == false && snakeBodyPoints[index][0].Y < MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
                if (snakeBodyPoints[index][0].Y == MapGenerator.sizeOfArray - 2)
                {
                    hitBottom[index] = true;
                    moveRight[index] = true;
                }
            }
            else if (moveLeft[index] == false && moveRight[index] == false && hitBottom[index] == true && snakeBodyPoints[index][0].Y > 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
                if (snakeBodyPoints[index][0].Y == 1)
                {
                    hitBottom[index] = false;
                    moveRight[index] = true;
                }
            }
            else if (moveLeft[index] == false && moveRight[index] == true && snakeBodyPoints[index][0].X < MapGenerator.sizeOfArray - 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y);
                moveRight[index] = false;
            }
            else if (moveLeft[index] == true && snakeBodyPoints[index][0].X > 0)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X - 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].Y == 0)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
                moveLeft[index] = false;
            }
            else if (snakeBodyPoints[index][0].Y == MapGenerator.sizeOfArray - 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
                moveLeft[index] = false;
            }
            else if (snakeBodyPoints[index][0].Y == 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
                moveLeft[index] = true;
                moveRight[index] = false;
            }
            else if (snakeBodyPoints[index][0].Y == MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
                moveLeft[index] = true;
                moveRight[index] = false;
            }
        }

        /// <summary>
        /// Moves to closest food, contracts if next move is over snake.
        /// </summary>
        /// <param name="objective"></param>
        static void PathFind3(Point objective, int index)
        {
            if (snakeBodyPoints[index][0].X < objective.X && !CollisionCheck(new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y), index))
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].X > objective.X && !CollisionCheck(new Point(snakeBodyPoints[index][0].X - 1, snakeBodyPoints[index][0].Y), index))
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X - 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].Y < objective.Y && !CollisionCheck(new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1), index))
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
            }
            else if (snakeBodyPoints[index][0].Y > objective.Y && !CollisionCheck(new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1), index))
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
            }
        }

        static void ConvertSnakeHeadToMap(int index)
        {
            MapGenerator.map[index][snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y] = MapTile.tileList[3];
        }

        static void ClearSnakeTailFromMap(int index)
        {
            MapGenerator.map[index][snakeBodyPoints[index][snakeBodyPoints[index].Count - 1].X, snakeBodyPoints[index][snakeBodyPoints[index].Count - 1].Y] = MapTile.tileList[1];
        }

        static void TryToEat(int index)
        {
            for (int i = 0; i < MapGenerator.FoodList[index].Count; i++)
            {
                if (MapGenerator.FoodList[index][i] == snakeBodyPoints[index][0])
                {
                    MapGenerator.FoodList[index].Remove(MapGenerator.FoodList[index][i]);
                    if (MapGenerator.FoodList[index].Count == 0)
                    {
                        Console.WriteLine($"Snake {index + 1} ran out of food!");
                        //FormControls.timerEnabled = false;
                        canMove[index] = false;
                    }
                    snakeBodyPoints[index].Add(snakeTailPoint[index]);
                    closestFood[index] = FindClosestsFood(index);
                    break;
                }
            }
        }

        public static void FollowSnakeHead(int index)
        {
            if (snakeBodyPoints[index][0].X >= 5 && snakeBodyPoints[index][0].X <= MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal[index])
            {
                FormControls.startingPointX[index] = snakeBodyPoints[index][0].X - 5;
            }
            else if (snakeBodyPoints[index][0].X < 5)
            {
                FormControls.startingPointX[index] = 0;
            }
            else if (snakeBodyPoints[index][0].X > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal[index])
            {
                FormControls.startingPointX[index] = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal[index];
            }
            if (snakeBodyPoints[index][0].Y >= 5 && snakeBodyPoints[index][0].Y <= MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical[index])
            {
                FormControls.startingPointY[index] = snakeBodyPoints[index][0].Y - 5;
            }
            else if (snakeBodyPoints[index][0].Y < 5)
            {
                FormControls.startingPointY[index] = 0;
            }
            else if (snakeBodyPoints[index][0].Y > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical[index])
            {
                FormControls.startingPointY[index] = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical[index];
            }
        }

        public static Point FindClosestsFood(int index)
        {
            double shortestDistance = double.MaxValue;
            Point closestFoodPoint = new Point();
            foreach (var food in MapGenerator.FoodList[index])
            {
                if (Math.Sqrt(Math.Pow(food.X - snakeBodyPoints[index][0].X, 2) + Math.Pow(food.Y - snakeBodyPoints[index][0].Y, 2)) < shortestDistance)
                {
                    shortestDistance = Math.Sqrt(Math.Pow(food.X - snakeBodyPoints[index][0].X, 2) + Math.Pow(food.Y - snakeBodyPoints[index][0].Y, 2));
                    closestFoodPoint = food;
                }
            }
            return closestFoodPoint;
        }

        static bool CollisionCheck(Point futurePos, int index)
        {
            for (int i = 0; i < snakeBodyPoints[index].Count; i++)
            {
                if (futurePos == snakeBodyPoints[index][i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}