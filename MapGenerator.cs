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
        public static bool useTextures = true;
        /// <summary>
        /// Fills the map with tiles.
        /// </summary>
        public static void FillMap()
        {
            for (int y = 0; y < AmountOfMaps; y++)
            {
                map.Add(new MapTile[sizeOfArray, sizeOfArray]);
                FoodList[y] = new List<Point>();
                Snake.snakeBodyPoints[y] = new List<Point>();
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
            int FoodAmount = random.Next(sizeOfArray / 3 - 10, sizeOfArray / 3 + 10);
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[index][x, y] = MapTile.tileList[1];
                }
            }
            //generates a random amount of food in the map and saves their locations to a list
            for (int food = 0; food < FoodAmount; food++)
            {
                FoodList[index].Add(new Point(random.Next(0,sizeOfArray), random.Next(0, sizeOfArray)));
            }
            //for each saved point of food actually draws and tells the map that the food is there
            foreach (var item in FoodList[index])
            {
                map[index][item.X, item.Y] = new MapTile("Food", 2);
            }
        }

        /// <summary>
        /// Draws the map and grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="startingPointX"></param>
        /// <param name="startingPointY"></param>
        public static void DrawMapAndGrid(object sender, PaintEventArgs e, int[] startingPointX, int[] startingPointY, int index)
        {
            for (int x = startingPointX[index]; x < startingPointX[index] + visibleMapSizeHorizontal[index]; x++)
            {
                for (int y = startingPointY[index]; y < startingPointY[index] + visibleMapSizeVertical[index]; y++)
                {
                    Brush brush = new SolidBrush(Color.White);
                    switch (map[index][x, y].ID)
                    {
                        case 0:
                            brush = new SolidBrush(Color.Black);
                            break;
                        case 1:
                            if (useTextures == true) brush = new TextureBrush(Properties.Resources.grass);
                            else brush = new SolidBrush(Color.YellowGreen);
                            break;
                        case 2:
                            if (useTextures == true) brush = new TextureBrush(Properties.Resources.food, System.Drawing.Drawing2D.WrapMode.Tile, new Rectangle(new Point(0, 0), new Size(20, 20)));
                            else brush = new SolidBrush(Color.Brown);
                            break;
                        case 3: brush = new SolidBrush(Color.DarkGreen);
                            break;
                        default:
                            break;
                    }
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(mapOffset.X + (x - startingPointX[index]) * sizeOfTile[index], mapOffset.Y + (y - startingPointY[index]) * sizeOfTile[index]), new Size(sizeOfTile[index], sizeOfTile[index])));

                }
            }
            for (int x = 0; x <= visibleMapSizeHorizontal[index]; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50,0,0,0)), new Point(mapOffset.X + x * sizeOfTile[index], mapOffset.Y), new Point(mapOffset.X + x * sizeOfTile[index], sizeOfTile[index] * visibleMapSizeVertical[index] + mapOffset.Y));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(mapOffset.X, mapOffset.Y + x * sizeOfTile[index]), new Point(sizeOfTile[index] * visibleMapSizeHorizontal[index] + mapOffset.X, mapOffset.Y + x * sizeOfTile[index]));
            }
        }

        public static void GenerateFood(int index)
        {
            Random random = new Random();
            Point food = new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray));
            map[index][food.X, food.Y] = new MapTile("Food", 2);
            FoodList[index].Add(food);
        }
    }
}