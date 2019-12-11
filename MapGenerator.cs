using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static void fillMap()
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int ForestPatchAmount = random.Next(SizeOfArray / 30 - 1, SizeOfArray / 30 + 1);
            //int GravelPatchAmount = random.Next(SizeOfArray / );
            //fills map with grass
            for (int x = 0; x < sizeOfArray; x++)
            {
                for (int y = 0; y < sizeOfArray; y++)
                {
                    map[x, y] = new MapTile("Grass", 1, null, Color.GreenYellow); // temp
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
                GenerateCircularBiomes(item, 4);
            }
        }
        public static void GenerateCircularBiomes(Biome biome, int sizeRadius)
        {
            for (double x = -sizeRadius; x <= sizeRadius; x=x+0.1)
            {
                if (true)
                {
                    map[Convert.ToInt32((x + biome.Location.X)), Convert.ToInt32((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y].AltColor = Color.Red;
                    Console.WriteLine(Convert.ToInt32((x + biome.Location.X)) + " " + Convert.ToInt16((Math.Sqrt(Math.Abs(x * x - sizeRadius * sizeRadius)))) + biome.Location.Y);
                }
                if (true)
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
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(mapOffset + x * sizeOfTile, mapOffset), new Point(mapOffset + x * sizeOfTile, sizeOfTile * visibleMapSizeVertical + mapOffset));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(mapOffset, mapOffset + x * sizeOfTile), new Point(sizeOfTile * visibleMapSizeHorizontal + mapOffset, mapOffset + x * sizeOfTile));
            }
        }
    }
}