using System;
using System.IO;
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

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
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
            MapGenerator.FillMap();
            timer1.Interval = 1000;
            timer1.Start();
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            MapGenerator.DrawMapAndGrid(sender, e, FormControls.startingPointX, FormControls.startingPointY);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        public static Keys keyHeld;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keyHeld = e.KeyData;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
        }
        //Save file button
        private void button1_Click(object sender, EventArgs e)
        {
            //Calls the save file method and pops up the directory dialog
            saveFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Fixes flickering
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            this.UpdateStyles();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Saves the file at the specified location
            SaveFile.SaveFileToTxt(Path.GetFullPath(saveFileDialog1.FileName), Path.GetFileName(saveFileDialog1.FileName));
        }
    }
}