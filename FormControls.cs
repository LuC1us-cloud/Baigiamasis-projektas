using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class FormControls
    {
        public static Point mouseDragStart;
        public static int startingPointX = 50;
        public static int startingPointY = 50;
        public static void MoveVisibleMap(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int moverX;
                int moverY;
                moverX = (e.X - mouseDragStart.X) / 20;
                moverY = (e.Y - mouseDragStart.Y) / 20;
                if (moverX != 0)
                {
                    if (moverX > 0)
                    {
                        moverX = 1;
                        if (startingPointX > 0)
                        {
                            startingPointX--;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverX = -1;
                        if (startingPointX < 90)
                        {
                            startingPointX++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.X += moverX * 20;
                }
                if (moverY != 0)
                {
                    if (moverY > 0)
                    {
                        moverY = 1;
                        if (startingPointY > 0)
                        {
                            startingPointY--;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverY = -1;
                        if (startingPointY < 90)
                        {
                            startingPointY++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.Y += moverY * 20;
                }
            }
        }

        public static int counter = 0;

        public static void SelectionUI(object sender, MouseEventArgs e)
        {
            counter++;
            if (counter == 4 * 20)
            {
                counter = 0;
            }
        }
    }
}