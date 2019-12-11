using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public static class FormControls
    {
        public static Point mouseDragStart;
        public static int startingPointX = 50;
        public static int startingPointY = 50;

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

        public static int scrollTolerance = 20;
        public static int selectedID = 0;

        /// <summary>
        /// Changes selected tile ID based on a counter which is triggered every scroll event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void TileSelection(object sender, MouseEventArgs e)
        {
            selectedID++;
            if (selectedID == MapTile.tileList.Count * scrollTolerance)
            {
                selectedID = 0;
            }
            //draw selection UI
        }
    }
}