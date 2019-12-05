using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            Form1.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverX = -1;
                        if (startingPointX < 90)
                        {
                            startingPointX++;
                            Form1.ActiveForm.Invalidate();
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
                            Form1.ActiveForm.Invalidate();
                        }
                    }
                    else
                    {
                        moverY = -1;
                        if (startingPointY < 90)
                        {
                            startingPointY++;
                            Form1.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.Y += moverY * 20;
                }
            }
        }
    }
}
