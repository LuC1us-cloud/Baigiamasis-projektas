using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class FormControls
    {
        public static Point mouseDragStart;
        public static int startingPointX = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeHorizontal;
        public static int startingPointY = MapGenerator.sizeOfArray - MapGenerator.visibleMapSizeVertical;

        /// <summary>
        /// Moves the visible map window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void MoveVisibleMap(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int moverX;
                int moverY;
                moverX = (e.X - mouseDragStart.X) / MapGenerator.sizeOfTile;
                moverY = (e.Y - mouseDragStart.Y) / MapGenerator.sizeOfTile;
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
                        if (startingPointX < MapGenerator.sizeOfArray - MapGenerator.sizeOfTile)
                        {
                            startingPointX++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.X += moverX * MapGenerator.sizeOfTile;
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
                        if (startingPointY < MapGenerator.sizeOfArray - MapGenerator.sizeOfTile)
                        {
                            startingPointY++;
                            Form.ActiveForm.Invalidate();
                        }
                    }
                    mouseDragStart.Y += moverY * MapGenerator.sizeOfTile;
                }
            }
        }
    }
}