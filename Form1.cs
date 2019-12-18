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
        public static bool TimerActive = true;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            FormControls.MoveVisibleMap(sender, e, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Sets the click coordinate
            FormControls.mouseDragStart = e.Location;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MapGenerator.FillMap();
            for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                Snake.GenerateSnakeHead(i);
                Snake.closestFood[i] = Snake.FindClosestsFood(i); 
            }
            timer1.Interval = 500;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                MapGenerator.DrawMapAndGrid(sender, e, FormControls.startingPointX, FormControls.startingPointY, i); 
            }
        }

        /// <summary>
        /// Save file button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            //Calls the save file method and pops up the directory dialog
            saveFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Fixes flickering
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
            UpdateStyles();
        }

        /// <summary>
        /// Saves the file at the specified location.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveFile.SaveFileToTxt(Path.GetFullPath(saveFileDialog1.FileName), Path.GetFileName(saveFileDialog1.FileName));
        }

        public void Timer1_Tick(object sender, EventArgs e)
        {
            if (TimerActive == false)
            {
                timer1.Stop();
                Invalidate();
            }
            else
            { // sitas NEVEIKS!!!!!!!!!!!!!!!!!!
                for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
                {
                    Snake.MoveSnake(Snake.closestFood[i]);
                    if (Snake.followSnakeHead == true)
                    {
                        Snake.FollowSnakeHead(i);
                    } 
                }
                Invalidate();
            }
        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
             for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                MapGenerator.GenerateFood(i);
                Snake.closestFood[i] = Snake.FindClosestsFood(i); 
            }
            Console.WriteLine(Snake.closestFood);
            TimerActive = true;
            timer1.Start();
        }

        private void GraphicsToggle_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            MapGenerator.useTextures = !MapGenerator.useTextures;
        }

        private void ObjectiveOutput_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            Snake.outputObjective = !Snake.outputObjective;
        }

        static int oldSliderValue = 0;

        private void GameSpeedSlider_Scroll(object sender, EventArgs e)
        {
            if (GameSpeedSlider.Value > oldSliderValue && timer1.Interval / 2 > 0)
            {
                timer1.Interval /= 2;
            }
            else if (GameSpeedSlider.Value < oldSliderValue && timer1.Interval < 500)
            {
                timer1.Interval *= 2;
            }
            oldSliderValue = GameSpeedSlider.Value;
        }

        private void TrackingToggle_Click(object sender, EventArgs e)
        {
            Snake.followSnakeHead = !Snake.followSnakeHead;
        }
    }
}