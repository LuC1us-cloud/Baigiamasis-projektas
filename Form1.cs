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

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Flicker fix
            SetStyle(
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer,
                    true);
            UpdateStyles(); 
            #endregion
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MapGenerator.FillMap();
            for (int i = 0; i < MapGenerator.amountOfMaps; i++)
            {
                Snake.GenerateSnakeHead(i);
                Snake.closestFood[i] = Snake.FindClosestsFood(i); 
            }
            timer1.Interval = 500;
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < MapGenerator.amountOfMaps; i++)
                MapGenerator.DrawMapAndGrid(e, FormControls.startingPointX, FormControls.startingPointY, i); 
            GUI.DrawTimer(e.Graphics, Convert.ToString(tick));
        }

        private static int tick = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < MapGenerator.amountOfMaps; i++)
                if (Snake.canMove[i])
                {
                    Snake.MoveSnake(Snake.closestFood[i], i);
                    if (Snake.followSnakeHead[i] == true)
                        Snake.FollowSnakeHead(i);
                    Invalidate();
                    tick++;
                }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            FormControls.mouseDragStart = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            FormControls.MoveVisibleMap(sender, e);
        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            for (int i = 0; i < MapGenerator.amountOfMaps; i++)
            {
                MapGenerator.GenerateFood(i);
                Snake.closestFood[i] = Snake.FindClosestsFood(i);
                Snake.canMove[i] = true;
            }
        }

        private void GraphicsToggle_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            MapGenerator.useTextures = !MapGenerator.useTextures;
        }

        private void TrackingToggle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MapGenerator.amountOfMaps; i++)
                Snake.followSnakeHead[i] = !Snake.followSnakeHead[i];
        }

        private static int oldSliderValue = 0;

        private void GameSpeedSlider_Scroll(object sender, EventArgs e)
        {
            if (GameSpeedSlider.Value > oldSliderValue && timer1.Interval / 2 > 0)
                timer1.Interval /= 2;
            else if (GameSpeedSlider.Value < oldSliderValue && timer1.Interval < 500)
                timer1.Interval *= 2;
            oldSliderValue = GameSpeedSlider.Value;
        }
    }
}