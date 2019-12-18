using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class MapGenerator
    {
        public static int AmountOfMaps = 2;
        public static int sizeOfArray = 40;
        public static int [] sizeOfTile = { 20, 20 };
        public static Point mapOffset = new Point( 10, 10 );
        public static List<MapTile[,]> map = new List<MapTile[,]>();
        public static List<Point>[] FoodList = new List<Point>[AmountOfMaps];
        public static int [] visibleMapSizeHorizontal = { 20, 20 };
        public static int [] visibleMapSizeVertical = { 20, 20 };
        public static bool useTextures = false;
        public static int SizeOfOneMap = 530;

        /// <summary>
        /// Initiates lists and arrays, then fills the maps with tiles
        /// </summary>
        public static void FillMap()
        {
            for (int y = 0; y < AmountOfMaps; y++)
            {
                map.Add(new MapTile[sizeOfArray, sizeOfArray]);
                FoodList[y] = new List<Point>();
                Snake.snakeBodyPoints[y] = new List<Point>();
                Snake.timeSpentAlive[y] = 1;
            }
                for (int i = 0; i < AmountOfMaps; i++)
                {
                    FillMapFirstTime(i);
                }
        }

        public static void FillMapFirstTime(int index)
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[index][x, y] = MapTile.tileList[1];
                }
            }
            GenerateFood(index);
        }

        public static void GenerateFood(int index)
        {
            Random random = new Random();
            int FoodAmount = random.Next(sizeOfArray / 3 - 10, sizeOfArray / 3 + 10);
            //generates a random amount of food in the map and saves their locations to a list
            for (int food = 0; food < FoodAmount; food++)
            {
                FoodList[index].Add(new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray)));
            }
            //for each saved point of food actually draws and tells the map that the food is there
            foreach (var item in FoodList[index])
            {
                map[index][item.X, item.Y] = new MapTile("Food", 2);
            }
        }
    }
}