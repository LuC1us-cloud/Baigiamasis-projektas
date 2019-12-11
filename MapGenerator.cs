using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class MapGenerator
    {
        public static int sizeOfArray = 110;
        public static int sizeOfTile = 20;
        public static int mapOffset = 10;
        public static MapTile[,] map = new MapTile[sizeOfArray, sizeOfArray];
        static List<Biome> biomes = new List<Biome>();
        static int visibleMapSizeHorizontal = 20;
        static int visibleMapSizeVertical = 20;

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
            int color;
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
                        color = Convert.ToInt32(vs[2]);
                        Console.WriteLine(name + " " + id + " " + color);
                        map[x, y] = new MapTile(name, id, Color.FromArgb(color));
                    }
                }
            }
        }
        public static void FillMapFirstTime()
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int ForestPatchAmount = random.Next(sizeOfArray / 30 - 1, sizeOfArray / 30 + 1);
            //int GravelPatchAmount = random.Next(SizeOfArray / );
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[x, y] = new MapTile("Grass", 1,  Color.GreenYellow); // temp
                }
            }
            //Generates Forests and their center points are saved (will be)
            for (int forest = 0; forest < ForestPatchAmount; forest++)
            {
                biomes.Add(new Biome("Forest", new Point(random.Next(0, sizeOfArray), random.Next(0, sizeOfArray))));
            }
            //Generates gravel pathches 
            //for (int gravel = 0; gravel < GravelPatchAmount; gravel++)
            {
                
            }
            //Prints names and locations of all special biomes on map
            foreach (var item in biomes)
            {
                Console.WriteLine(item.Name + " at: " + item.Location);
                map[item.Location.X, item.Location.Y].AltColor = Color.Black;
                GenerateCircularBiomes(item, 10);
            }
            Console.WriteLine(map[0,0].ToString());
            Console.WriteLine(map[0, 1].ToString());
        }
        public static void GenerateCircularBiomes(Biome biome, int sizeRadius)
        {
            for (double x = -sizeRadius; x <= sizeRadius; x=x+0.1)
            {
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[Convert.ToInt32((x + biome.Location.X)), Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].AltColor = Color.Red;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + Convert.ToInt16((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
                if (Convert.ToInt32((x + biome.Location.X)) >= 0 && Convert.ToInt32((x + biome.Location.X)) < sizeOfArray && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y >= 0 && -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y < sizeOfArray)
                {
                    map[Convert.ToInt32((x + biome.Location.X)), -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].AltColor = Color.Red;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + -Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
            }
        }
        public static void DrawMapAndGrid(object sender, PaintEventArgs e, int startingPointX, int startingPointY)
        {
            for (int x = startingPointX; x < startingPointX + visibleMapSizeHorizontal; x++)
            {
                for (int y = startingPointY; y < startingPointY + visibleMapSizeVertical; y++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(map[x, y].AltColor), new Rectangle(new Point(mapOffset + (x - startingPointX) * sizeOfTile, mapOffset + (y - startingPointY) * sizeOfTile), new Size(sizeOfTile, sizeOfTile)));
                }
            }
            for (int x = 0; x <= visibleMapSizeHorizontal; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50,0,0,0)), new Point(mapOffset + x * sizeOfTile, mapOffset), new Point(mapOffset + x * sizeOfTile, sizeOfTile * visibleMapSizeVertical + mapOffset));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(mapOffset, mapOffset + x * sizeOfTile), new Point(sizeOfTile * visibleMapSizeHorizontal + mapOffset, mapOffset + x * sizeOfTile));
            }
        }
    }
}