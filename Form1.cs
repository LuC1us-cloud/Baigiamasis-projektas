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
            if (e.X < (MapGenerator.SizeOfOneMap + MapGenerator.mapOffset.X + MapGenerator.visibleMapSizeHorizontal[0] * MapGenerator.sizeOfTile[0]) / 2)
            {
                FormControls.MoveVisibleMap(sender, e, 0);
            }
            else
            {
                FormControls.MoveVisibleMap(sender, e, 1);
            }
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
                GUI.DrawMapAndGrid(sender, e, FormControls.startingPointX, FormControls.startingPointY, i); 
            }
            GUI.DrawTimer(e.Graphics, Convert.ToString(tick));
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

        static int tick = 0;

        public void Timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                if (Snake.canMove[i])
                {
                    Snake.MoveSnake(Snake.closestFood[i], i);
                    if (Snake.outputObjective == true)
                    {
                        Console.WriteLine(Snake.closestFood);
                    }
                    if (Snake.followSnakeHead == true)
                    {
                        Snake.FollowSnakeHead(i);
                    }
                    Invalidate();
                    tick++;
                }
            }
        }

        private void FoodButton_Click(object sender, EventArgs e)
        {
            ActiveControl = null;
            for (int i = 0; i < MapGenerator.AmountOfMaps; i++)
            {
                MapGenerator.GenerateFood(i);
                Snake.closestFood[i] = Snake.FindClosestsFood(i);
                Snake.canMove[i] = true;
            }
            Invalidate();
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