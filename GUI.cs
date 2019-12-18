using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    class GUI
    {

        /// <summary>
        /// Draws the map and grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="startingPointX"></param>
        /// <param name="startingPointY"></param>
        public static void DrawMapAndGrid(object sender, PaintEventArgs e, int[] startingPointX, int[] startingPointY, int index)
        {
            for (int x = startingPointX[index]; x < startingPointX[index] + MapGenerator.visibleMapSizeHorizontal[index]; x++)
            {
                for (int y = startingPointY[index]; y < startingPointY[index] + MapGenerator.visibleMapSizeVertical[index]; y++)
                {
                    Brush brush = new SolidBrush(Color.White);
                    switch (MapGenerator.map[index][x, y].ID)
                    {
                        case 0:
                            brush = new SolidBrush(Color.Black);
                            break;
                        case 1:
                            if (MapGenerator.useTextures == true) brush = new TextureBrush(Properties.Resources.grass);
                            else brush = new SolidBrush(Color.YellowGreen);
                            break;
                        case 2:
                            if (MapGenerator.useTextures == true) brush = new TextureBrush(Properties.Resources.food, System.Drawing.Drawing2D.WrapMode.Tile, new Rectangle(new Point(0, 0), new Size(20, 20)));
                            else brush = new SolidBrush(Color.Brown);
                            break;
                        case 3:
                            brush = new SolidBrush(Color.DarkGreen);
                            break;
                        default:
                            break;
                    }
                    e.Graphics.FillRectangle(brush, new Rectangle(new Point(index * MapGenerator.SizeOfOneMap + MapGenerator.mapOffset.X + (x - startingPointX[index]) * MapGenerator.sizeOfTile[index], MapGenerator.mapOffset.Y + (y - startingPointY[index]) * MapGenerator.sizeOfTile[index]), new Size(MapGenerator.sizeOfTile[index], MapGenerator.sizeOfTile[index])));


                }
            }
            for (int x = 0; x <= MapGenerator.visibleMapSizeHorizontal[index]; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(index * MapGenerator.SizeOfOneMap + MapGenerator.mapOffset.X + x * MapGenerator.sizeOfTile[index], MapGenerator.mapOffset.Y), new Point(index * MapGenerator.SizeOfOneMap + MapGenerator.mapOffset.X + x * MapGenerator.sizeOfTile[index], MapGenerator.sizeOfTile[index] * MapGenerator.visibleMapSizeVertical[index] + MapGenerator.mapOffset.Y));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(50, 0, 0, 0)), new Point(index * MapGenerator.SizeOfOneMap + MapGenerator.mapOffset.X, MapGenerator.mapOffset.Y + x * MapGenerator.sizeOfTile[index]), new Point(index * MapGenerator.SizeOfOneMap + MapGenerator.sizeOfTile[index] * MapGenerator.visibleMapSizeHorizontal[index] + MapGenerator.mapOffset.X, MapGenerator.mapOffset.Y + x * MapGenerator.sizeOfTile[index]));

            }
        }

        public static void DrawTimer(Graphics e, string s)
        {
            string a = "Moves: " + s;
            e.DrawString(a, new Font("Times new roman", 16), new SolidBrush(Color.Black), new Point(Form1.ActiveForm.Size.Width/2-a.Length*6, MapGenerator.mapOffset.Y + MapGenerator.visibleMapSizeVertical[0] * MapGenerator.sizeOfTile[0]));
        }
    }
}
