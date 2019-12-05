using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace movable_2dmap
{
    public partial class Form1 : Form
    {
        static MapTile[,] map = new MapTile[110, 110];
        static List<Biome> biomes = new List<Biome>();
        static Point mouseDragStart;
        static int startingPointX = 50;
        static int startingPointY = 50;
        public Form1()
        {

            InitializeComponent();
            Invalidate();
        }
        public void fillMap()
        {
            // Possible Tiles: Grass,  Gravel, Tree, Stone, High stone (mountain), <(Technical tiles)>.
            Random random = new Random();
            int SizeOfArray = 110;
            Console.WriteLine(SizeOfArray);
            int ForestAmount = random.Next(SizeOfArray/30 - 1, SizeOfArray/30 + 1);
            Console.WriteLine(ForestAmount);
            //fills map with grass
            for (int x = 0; x < 110; x++)
            {
                for (int y = 0; y < 110; y++)
                {
                    map[x, y] = new MapTile("Grass", 1, null, Color.GreenYellow);
                }
            }
            //Generates Forests and their center points are saved (will be)
            for (int forest = 0; forest < ForestAmount; forest++)
            {
                biomes.Add(new Biome("Forest", new Point(random.Next(0, 110), random.Next(0, 110))));
            }
            foreach (var item in biomes)
            {
                Console.WriteLine(item.Location);
                map[item.Location.X, item.Location.Y].AltColor = Color.Black;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int moverX;
                int moverY;
                moverX = (e.X - mouseDragStart.X) / 20;
                moverY = (e.Y - mouseDragStart.Y) / 20;
                if(moverX != 0)
                {
                    if (moverX > 0)
                    {
                        moverX = 1;
                        if(startingPointX > 0)
                        {
                            startingPointX--;
                            Invalidate();
                        }
                    }
                    else
                    {
                        moverX = -1;
                        if(startingPointX < 100)
                        {
                            startingPointX++;
                            Invalidate();
                        }
                    }
                    mouseDragStart.X += moverX * 20;
                }
                if(moverY != 0)
                {
                    if (moverY > 0)
                    {
                        moverY = 1;
                        if (startingPointY > 0)
                        {
                            startingPointY--;
                            Invalidate();
                        }
                    }
                    else
                    {
                        moverY = -1;
                        if (startingPointY < 100)
                        {
                            startingPointY++;
                            Invalidate();
                        }
                    }
                    mouseDragStart.Y += moverY * 20;
                }
                Console.WriteLine(moverX + " " + moverY + " || " + startingPointX + " " + startingPointY);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDragStart = e.Location;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            fillMap();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int x = startingPointX; x < startingPointX + 10; x++)
            {
                for (int y = startingPointY; y < startingPointY + 10; y++)
                {
                    e.Graphics.FillRectangle(new SolidBrush(map[x, y].AltColor), new Rectangle(new Point(10 + (x - startingPointX) * 20, 10 + (y - startingPointY) * 20), new Size(20, 20)));
                }
            }
            for (int x = 0; x <= 10; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(10 + x * 20, 10), new Point(10 + x * 20, 210));
                e.Graphics.DrawLine(new Pen(Color.Black), new Point(10, 10 + x * 20), new Point(210, 10 + x * 20));
            }
        }
    }
}
