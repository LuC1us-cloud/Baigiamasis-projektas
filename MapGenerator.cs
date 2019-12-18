using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class MapGenerator
    {
        public static int AmountOfMaps = 2;
        public static int sizeOfArray = 40;
        public static int [] sizeOfTile = { 20, 20 };
        public static int mapOffset = 10;
        public static MapTile[][,] map = new MapTile[AmountOfMaps][,];
        public static List<Biome>[] Biomes = new List<Biome>[AmountOfMaps];
        public static List<Point>[] FoodList = new List<Point>[AmountOfMaps];
        public static int [] visibleMapSizeHorizontal = { 20, 20 };
        public static int [] visibleMapSizeVertical = { 20, 20 };
        public static bool useTextures = true;
        /// <summary>
        /// Fills the map with tiles.
        /// </summary>
        public static void FillMap()
        {
            if (File.Exists(@"demo.txt"))
            {
                for (int i = 0; i < AmountOfMaps; i++)
                {
                    FillMapFromFile(i);
                }
            }
            else
            {
                for (int i = 0; i < AmountOfMaps; i++)
                {
                    FillMapFirstTime(i);

                }    
            }
        }

        public static void FillMapFromFile(int index)
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
                        map[index][x, y] = new MapTile(name, id);
                    }
                }
            }
        }

        public static void FillMapFirstTime(int index)
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int ForestPatchAmount = random.Next(sizeOfArray / 30 - 1, sizeOfArray / 30 + 1);
            int FoodAmount = random.Next(sizeOfArray / 3 - 10, sizeOfArray / 3 + 10);
            //int GravelPatchAmount = random.Next(SizeOfArray / );
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[index][x, y] = new MapTile("Grass", 1); // temp
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

        public static void GenerateCircularBiomes(Biome biome, int sizeRadius, int index)
        {
            for (double x = -sizeRadius; x <= sizeRadius; x += 0.1)
            {
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[index][Convert.ToInt32((x + biome.Location.X)), Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].ID = 0;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + Convert.ToInt16((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[index][Convert.ToInt32((x + biome.Location.X)), -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].ID = 0;
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
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(mapOffset + (x - startingPointX[index]) * sizeOfTile[index], mapOffset + (y - startingPointY[index]) * sizeOfTile[index]), new Size(sizeOfTile[index], sizeOfTile[index])));
                }
            }
            for (int x = 0; x <= visibleMapSizeHorizontal[index]; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50,0,0,0)), new Point(mapOffset + x * sizeOfTile[index], mapOffset), new Point(mapOffset + x * sizeOfTile[index], sizeOfTile[index] * visibleMapSizeVertical[index] + mapOffset));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(mapOffset, mapOffset + x * sizeOfTile[index]), new Point(sizeOfTile[index] * visibleMapSizeHorizontal[index] + mapOffset, mapOffset + x * sizeOfTile[index]));
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