using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public class MapTile
    {
        public string Name { get; }
        public int ID { get; }
        public Image Image { get; }
        public Color AltColor { get; set; } //Alternative color, if Image file doesn't exist
        //MetaData metaData { get; }
        public MapTile(string name, int id, Image image, Color color)
        {
            Name = name;
            ID = id;
            Image = image;
            AltColor = color;
        }

        public static List<MapTile> tileList = new List<MapTile>{
            new MapTile("Null", 0, null, Color.White),
            new MapTile("Grass", 1, null, Color.GreenYellow),
            new MapTile("Bluestone", 2, null, Color.DarkBlue),
            new MapTile("Torch", 3, null, Color.Magenta)
        };

        public static List<Point> placedBluestone = new List<Point>();
        public static List<Point> placedTorches = new List<Point>();

        /// <summary>
        /// Removes or adds a tile on the map depending on mouse button pressed. Left mouse button press adds a tile, while a right 
        /// mouse button press removes a tile from the map.
        /// </summary>
        /// <param name="e">used to retrieve mouse press location</param>
        public static void ProcessTileChange(MouseEventArgs e)
        {
            int i = (e.X + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointX, j = (e.Y + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointY;
            if (e.Button == MouseButtons.Left && MapGenerator.map[i, j].ID == 1)
            {
                MapGenerator.map[i, j] = tileList[FormControls.selectedID / FormControls.scrollTolerance];
                if (FormControls.selectedID / FormControls.scrollTolerance == 2)
                {
                    placedBluestone.Add(new Point(i, j));
                }
                else if (FormControls.selectedID / FormControls.scrollTolerance == 3)
                {
                    placedTorches.Add(new Point(i, j));
                    Bluestone.UpdateTorches();
                }
                Bluestone.UpdateBluestone();
                Form.ActiveForm.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (MapGenerator.map[i, j].Name == "Bluestone" || MapGenerator.map[i, j].Name == "Bluestone_powered")
                {
                    MapGenerator.map[i, j] = tileList[1];
                    placedBluestone.Remove(new Point(i, j));
                    Bluestone.UpdateBluestone();
                }
                else if (MapGenerator.map[i, j].Name == "Torch" || MapGenerator.map[i, j].Name == "Torch_unpowered")
                {
                    MapGenerator.map[i, j] = tileList[1];
                    placedTorches.Remove(new Point(i, j));
                    Bluestone.UpdateTorches();
                    Bluestone.UpdateBluestone();
                }
                MapGenerator.map[i, j] = tileList[1];
                Form.ActiveForm.Invalidate();
            }
        }
    }
}