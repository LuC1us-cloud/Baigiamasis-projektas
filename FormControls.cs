using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class FormControls
    {
        public static Point mouseDragStart;
        public static int []startingPointX = { 10, 10 };
        public static int []startingPointY = { 10, 10 };
        public static bool timerEnabled = true;
        /// <summary>
        /// Moves the visible map window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MoveVisibleMap(object sender, MouseEventArgs e, int index)
        {
            if (e.Button == MouseButtons.Left)
            {
                int moverX;
                int moverY;
                moverX = (e.X - mouseDragStart.X) / MapGenerator.sizeOfTile[index];
                moverY = (e.Y - mouseDragStart.Y) / MapGenerator.sizeOfTile[index];
                if (moverX != 0)
                {
                    if (moverX > 0)
                    {
                        moverX = 1;
                        if (startingPointX[index] > 0)
                        {
                            startingPointX[index]--;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverX = -1;
                        if (startingPointX[index] < MapGenerator.sizeOfArray - MapGenerator.sizeOfTile[index])
                        {
                            startingPointX[index]++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.X += moverX * MapGenerator.sizeOfTile[index];
                }
                if (moverY != 0)
                {
                    if (moverY > 0)
                    {
                        moverY = 1;
                        if (startingPointY[index] > 0)
                        {
                            startingPointY[index]--;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverY = -1;
                        if (startingPointY[index] < MapGenerator.sizeOfArray - MapGenerator.sizeOfTile[index])
                        {
                            startingPointY[index]++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.Y += moverY * MapGenerator.sizeOfTile[index];
                }
            }
        }
    }
}