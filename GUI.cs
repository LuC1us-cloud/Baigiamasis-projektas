using System.Drawing;

namespace movable_2dmap
{
    class GUI
    {
        public static void DrawTimer(Graphics e, string s)
        {
            e.DrawString(s, new Font("Times new roman", 16), new SolidBrush(Color.Black), new Point(MapGenerator.mapOffset.X, MapGenerator.mapOffset.Y + MapGenerator.visibleMapSizeVertical[0] * MapGenerator.sizeOfTile[0]));
        }
    }
}
