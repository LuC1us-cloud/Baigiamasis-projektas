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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Moves the visible map window
            FormControls.MoveVisibleMap(sender, e);
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
    }
}