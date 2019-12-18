using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class MapGenerator
    {
        public static int sizeOfArray = 40;
        public static int sizeOfTile = 20;
        public static Point mapOffset = new Point(10, 10);
        public static MapTile[,] map = new MapTile[sizeOfArray, sizeOfArray];
        static List<Biome> biomes = new List<Biome>();
        static List<MapTile> FoodList = new List<MapTile>();
        public static List<Point> foodPoints = new List<Point>();
        public static int visibleMapSizeHorizontal = 20;
        public static int visibleMapSizeVertical = 20;
        public static bool useTextures = true;

        /// <summary>
        /// Fills the map with tiles.
        /// </summary>
        public static void FillMap()
        {
            if (File.Exists(@"demo.txt"))
            {
                FillMapFromFile();
            }
            else
            {
                FillMapFirstTime();
            }
        }

        public static void FillMapFromFile()
        {
            string name;
            int id;
            string temp = "";
            string[] vs;
            using (StreamReader sr = File.OpenText(@"demo.txt"))
            {
                for (int x = 0; x < sizeOfArray; x++)
                {
                    for (int y = 0; y < sizeOfArray; y++)
                    {
                        temp = sr.ReadLine();
                        vs = temp.Split();
                        name = vs[0];
                        id = Convert.ToInt32(vs[1]);
                        Console.WriteLine(name + " " + id);
                        map[x, y] = new MapTile(name, id);
                    }
                }
            }
        }

        static int foodAmount;

        public static void FillMapFirstTime()
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int ForestPatchAmount = random.Next(sizeOfArray / 30 - 1, sizeOfArray / 30 + 1);
            foodAmount = random.Next(sizeOfArray / 3 - 10, sizeOfArray / 3 + 10);
            //int GravelPatchAmount = random.Next(SizeOfArray / );
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[x, y] = new MapTile("Grass", 1); // temp
                }
            }
            for (int food = 0; food < foodAmount; food++)
            {
                FoodList.Add(new MapTile("Food",2));
            }
            //Generates Forests and their center points are saved (will be)
            for (int forest = 0; forest < ForestPatchAmount; forest++)
            {
                biomes.Add(new Biome("Forest", new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray))));
            }
            //Prints names and locations of all special biomes on map
            foreach (var item in FoodList)
            {
                Point food = new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray));
                map[food.X, food.Y] = item;
                foodPoints.Add(food);
            }
        }

        public static void GenerateCircularBiomes(Biome biome, int sizeRadius)
        {
            for (double x = -sizeRadius; x <= sizeRadius; x += 0.1)
            {
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[Convert.ToInt32((x + biome.Location.X)), Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].ID = 0;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + Convert.ToInt16((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[Convert.ToInt32((x + biome.Location.X)), -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].ID = 0;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
            }
        }

        /// <summary>
        /// Draws the map and grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="startingPointX"></param>
        /// <param name="startingPointY"></param>
        public static void DrawMapAndGrid(object sender, PaintEventArgs e, int startingPointX, int startingPointY)
        {
            for (int x = startingPointX; x < startingPointX + visibleMapSizeHorizontal; x++)
            {
                for (int y = startingPointY; y < startingPointY + visibleMapSizeVertical; y++)
                {
                    Brush brush = new SolidBrush(Color.White);
                    switch (map[x, y].ID)
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
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(mapOffset.X + (x - startingPointX) * sizeOfTile, mapOffset.Y + (y - startingPointY) * sizeOfTile), new Size(sizeOfTile, sizeOfTile)));
                }
            }
            for (int x = 0; x <= visibleMapSizeHorizontal; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50,0,0,0)), new Point(mapOffset.X + x * sizeOfTile, mapOffset.Y), new Point(mapOffset.X + x * sizeOfTile, sizeOfTile * visibleMapSizeVertical + mapOffset.Y));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(mapOffset.X, mapOffset.Y + x * sizeOfTile), new Point(sizeOfTile * visibleMapSizeHorizontal + mapOffset.X, mapOffset.Y + x * sizeOfTile));
            }
        }

        public static void GenerateFood()
        {
            Random random = new Random();
            Point food = new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray));
            if (!Snake.snakeBodyPoints.Contains(food))
            {
                map[food.X, food.Y] = new MapTile("Food", 2);
                foodPoints.Add(food);
            }
            else
            {
                GenerateFood();
            } 
        }
    }
}