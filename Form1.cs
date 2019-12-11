using System;
using System.Windows.Forms;

namespace movable_2dmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static MouseEventArgs mouse;
        public static Keys keyHeld;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Moves the visible map window
            FormControls.MoveVisibleMap(sender, e);
            mouse = e;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Sets the click coordinate
            FormControls.mouseDragStart = e.Location;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Fills the map with tiles
            MapGenerator.fillMap();
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Draws the map and grid
            MapGenerator.DrawMapAndGrid(sender, e, FormControls.startingPointX, FormControls.startingPointY);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            MapTile.ProcessTileChange(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyHeld = e.KeyData;
            if (keyHeld == Keys.D)
            {
                MapTile.ProcessTileChange(mouse);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Bluestone.UpdateDelayers();
            Bluestone.UpdateBluestone();
            Invalidate();
        }
    }
}