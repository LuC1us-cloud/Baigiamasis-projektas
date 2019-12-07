using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class MapGenerator
    {
        public static int SizeOfArray = 110;
        public static MapTile[,] map = new MapTile[SizeOfArray, SizeOfArray];
        static List<Biome> biomes = new List<Biome>();
        static int visibleMapSizeHorizontal = 20;
        static int visibleMapSizeVertical = 20;

        public static void fillMap()
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int ForestAmount = random.Next(SizeOfArray / 30 - 1, SizeOfArray / 30 + 1);
            //fills map with grass
            for (int x = 0; x < 110; x++)
            {
                for (int y = 0; y < 110; y++)
                {
                    map[x, y] = new MapTile("Grass", 1, null, Color.GreenYellow); // temp
                }
            }
            //Generates Forests and their center points are saved (will be)
            for (int forest = 0; forest < ForestAmount; forest++)
            {
                biomes.Add(new Biome("Forest", new Point(random.Next(0, 110), random.Next(0, 110))));
            }
            foreach (var item in biomes)
            {
                map[item.Location.X, item.Location.Y].AltColor = Color.Black;
            }
        }
        public static void DrawMapAndGrid(object sender, PaintEventArgs e, int startingPointX, int startingPointY)
        {
            for (int x = startingPointX; x < startingPointX + visibleMapSizeHorizontal; x++)
            {
                for (int y = startingPointY; y < startingPointY + visibleMapSizeVertical; y++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(map[x, y].AltColor), new Rectangle(new Point(10 + (x - startingPointX) * 20, 10 + (y - startingPointY) * 20), new Size(20, 20)));
                }
            }
            for (int x = 0; x <= visibleMapSizeHorizontal; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(10 + x * 20, 10), new Point(10 + x * 20, 410));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(10, 10 + x * 20), new Point(410, 10 + x * 20));
            }
        }
    }
}