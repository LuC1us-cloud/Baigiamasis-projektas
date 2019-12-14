using System;
using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Snake
    {
        static List<Point> snakeBodyPoints = new List<Point>() { };
        public static Point closestFood = new Point();
        
        public static void GenerateSnakeHead()
        {
            Random random = new Random();
            snakeBodyPoints.Add(new Point(random.Next(0, MapGenerator.sizeOfArray), random.Next(0, MapGenerator.sizeOfArray)));
        }

        public static void MoveToObjective(Point objective)
        {
            ClearSnakeFromMap();
            for (int i = snakeBodyPoints.Count - 1; i > 0; i--)
            {
                snakeBodyPoints[i] = snakeBodyPoints[i - 1];
            }
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
            if (snakeBodyPoints[0] == objective)
            {
                Eat(objective);
            }
            ConvertSnakeDataToMap();
        }

        static void ConvertSnakeDataToMap()
        {
            foreach (var point in snakeBodyPoints)
            {
                MapGenerator.map[point.X, point.Y] = MapTile.tileList[3];
            }
        }

        static void ClearSnakeFromMap()
        {
            foreach (var point in snakeBodyPoints)
            {
                MapGenerator.map[point.X, point.Y] = MapTile.tileList[1];
            }
        }

        static void Eat(Point foodPoint)
        {
            MapGenerator.map[foodPoint.X, foodPoint.Y] = MapTile.tileList[1];
            MapGenerator.foodPoints.Remove(foodPoint);
            snakeBodyPoints.Add(foodPoint);
            closestFood = FindClosestsFood();
        }

        public static void FollowSnakeHead()
        {
            FormControls.startingPointX = snakeBodyPoints[0].X;
            if (FormControls.startingPointX > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal)
            {
                FormControls.startingPointX = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal;
            }
            FormControls.startingPointY = snakeBodyPoints[0].Y;
            if (FormControls.startingPointY > MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical)
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
    }
}