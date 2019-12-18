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
        
        public static void GenerateSnakeHead(int index)
        {
            Random random = new Random();
            snakeBodyPoints[index].Add(new Point(random.Next(0, MapGenerator.sizeOfArray), random.Next(0, MapGenerator.sizeOfArray)));
        }

        public static void MoveSnake(Point objective)
        {
            for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                ClearSnakeTailFromMap(i);
                snakeTailPoint[i] = snakeBodyPoints[i][snakeBodyPoints[i].Count - 1];
                for (int y = snakeBodyPoints[i].Count - 1; y > 0; y--)
                {
                    snakeBodyPoints[i][y] = snakeBodyPoints[i][y - 1];
                }
                PathFind(objective, i);
                //PathFind2();
                CollisionCheck(i);
                TryToEat(i);
                ConvertSnakeHeadToMap(i); 
            }
        }

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
        static void PathFind2(int index)
        {
            if (snakeBodyPoints[index][0].Y == 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y);
                hitBottom = false;
            }
            else if (snakeBodyPoints[index][0].Y == MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X + 1, snakeBodyPoints[index][0].Y);
                hitBottom = true;
            }
            if (hitSide == false && hitBottom == false && snakeBodyPoints[index][0].Y < MapGenerator.sizeOfArray - 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y + 1);
            }
            else if (snakeBodyPoints[0].Y == 1)
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X, snakeBodyPoints[index][0].Y - 1);
            }
            else if (snakeBodyPoints[0].Y == MapGenerator.sizeOfArray - 2)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
                moveLeft = true;
                moveRight = false;
            }
        }

        /// <summary>
        /// Moves to closest food, contracts if next move is over snake.
        /// </summary>
        /// <param name="objective"></param>
        static void PathFind3(Point objective)
        {
            if (snakeBodyPoints[0].X < objective.X && !CollisionCheck(new Point(snakeBodyPoints[0].X + 1, snakeBodyPoints[0].Y)))
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X + 1, snakeBodyPoints[0].Y);
            }
            else if (snakeBodyPoints[0].X > objective.X && !CollisionCheck(new Point(snakeBodyPoints[0].X - 1, snakeBodyPoints[0].Y)))
            {
                snakeBodyPoints[index][0] = new Point(snakeBodyPoints[index][0].X - 1, snakeBodyPoints[index][0].Y);
            }
            else if (snakeBodyPoints[index][0].X == 0)
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y + 1);
            }
            if (snakeBodyPoints[index][0].X == MapGenerator.sizeOfArray - 1 && (snakeBodyPoints[index][0].Y == 1 || snakeBodyPoints[index][0].Y == MapGenerator.sizeOfArray - 2))
            {
                snakeBodyPoints[0] = new Point(snakeBodyPoints[0].X, snakeBodyPoints[0].Y - 1);
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
                    MapGenerator.map[index][MapGenerator.FoodList[index][i].X, MapGenerator.FoodList[index][i].Y] = MapTile.tileList[3];
                    MapGenerator.FoodList[index].Remove(MapGenerator.FoodList[index][i]);
                    if (MapGenerator.FoodList[index].Count == 0)
                    {
                        Console.WriteLine("Game over!");
                        Form1.TimerActive = false;
                      
                        //Form1.ActiveForm.Close();
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
        static void CollisionCheck(int index)
        {
            for (int i = 1; i < snakeBodyPoints[index].Count; i++)
            {
                if (snakeBodyPoints[index][0] == snakeBodyPoints[index][i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}