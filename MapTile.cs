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

        public static List<Point> placedWires = new List<Point>();
        public static List<Point> placedTorches = new List<Point>();

        /// <summary>
        /// Removes or adds a tile on the map depending on mouse button pressed. Left mouse button press adds a tile, while a right 
        /// mouse button press removes a tile from the map.
        /// </summary>
        /// <param name="e">used to retrieve mouse press location</param>
        public static void ProcessTileChange(MouseEventArgs e)
        {
            int i = (e.X + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointX, j = (e.Y + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointY;
            if (e.Button == MouseButtons.Left)
            {
                Color tileColor = new Color();
                string name = "null";
                if (MapGenerator.map[i, j].ID == 1)
                {
                    switch (FormControls.selectedID / FormControls.scrollTolerance)
                    {
                        case 0:
                            tileColor = Color.White;
                            break;
                        case 1:
                            tileColor = Color.GreenYellow;
                            name = "Grass";
                            break;
                        case 2:
                            tileColor = Color.DarkBlue;
                            name = "Bluestone";
                            placedWires.Add(new Point(i, j));
                            break;
                        case 3:
                            tileColor = Color.Magenta;
                            name = "Torch";
                            placedTorches.Add(new Point(i, j));
                            break;
                        case 4:
                            tileColor = Color.Red;
                            name = "Inversor";
                            break;
                        case 5:
                            tileColor = Color.Orange;
                            name = "Pulsar";
                            placedTorches.Add(new Point(i, j));
                            break;
                        default:
                            break;
                    }
                    MapGenerator.map[i, j] = new MapTile(name, FormControls.selectedID / FormControls.scrollTolerance, null, tileColor);
                    Bluestone.UpdateTorches();
                    Bluestone.UpdateWires();
                    Form.ActiveForm.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (MapGenerator.map[i, j].Name == "Bluestone" && (MapGenerator.map[i, j].ID == 2 || MapGenerator.map[i, j].ID == 3))
                {
                    placedWires.Remove(new Point(i, j));
                    Bluestone.UnpowerWires(new Point(i, j));
                    Bluestone.UpdateWires();
                }
                else if (MapGenerator.map[i, j].Name == "Torch" && (MapGenerator.map[i, j].ID == 2 || MapGenerator.map[i, j].ID == 3))
                {
                    placedTorches.Remove(new Point(i, j));
                    Bluestone.UnpowerWires(new Point(i, j));
                    Bluestone.UpdateTorches();
                }
                MapGenerator.map[i, j] = new MapTile("Grass", 1, null, Color.GreenYellow);
                Form.ActiveForm.Invalidate();
            }
        }
    }
}