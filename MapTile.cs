using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public class MapTile
    {
        public string Name { get; }
        public int ID { get; }
        public Color AltColor { get; set; } //Alternative color, if Image file doesn't exist
        //MetaData metaData { get; }
        public MapTile(string name, int id, Color color)
        {
            Name = name;
            ID = id;
            AltColor = color;
        }

        public static List<MapTile> tileList = new List<MapTile>{
            new MapTile("Null", 0, Color.White),
            new MapTile("Grass", 1, Color.GreenYellow),
            new MapTile("Bluestone", 2, Color.DarkBlue),
            new MapTile("Torch", 3, Color.Magenta),
            new MapTile("Delayer", 4, Color.Gray)
        };
        public  override string ToString()
        {
            string temp = Name + " " + ID.ToString() + " " + AltColor.ToArgb();
            return temp;
        }
        /// <summary>
        /// Removes or adds a tile on the map depending on mouse button pressed. Left mouse button press adds a tile, while a right 
        /// mouse button press removes a tile from the map.
        /// </summary>
        /// <param name="e">used to retrieve mouse press location</param>
        public static void ProcessTileChange(MouseEventArgs e)
        {
            int i = (e.X + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointX, j = (e.Y + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointY;
            if ((Form1.keyHeld == Keys.D || e.Button == MouseButtons.Left) && MapGenerator.map[i, j].ID == 1)
            {
                MapGenerator.map[i, j] = tileList[FormControls.selectedID / FormControls.scrollTolerance];
                switch (FormControls.selectedID / FormControls.scrollTolerance)
                {
                    case 2:
                        Bluestone.placedBluestone.Add(new Point(i, j));
                        break;
                    case 3:
                        Bluestone.placedTorches.Add(new Point(i, j));
                        Bluestone.UpdateTorches();
                        break;
                    case 4:
                        Bluestone.placedDelayers.Add(new Point(i, j));
                        Bluestone.DetermineDelayerDirection(new Point(i, j));
                        break;
                    default:
                        break;
                }
                Bluestone.UpdateBluestone();
                Form.ActiveForm.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                switch (MapGenerator.map[i, j].Name)
                {
                    case "Bluestone":
                    case "Bluestone_powered":
                        MapGenerator.map[i, j] = tileList[1];
                        Bluestone.placedBluestone.Remove(new Point(i, j));
                        Bluestone.UpdateBluestone();
                        break;
                    case "Torch":
                    case "Torch_unpowered":
                        MapGenerator.map[i, j] = tileList[1];
                        Bluestone.placedTorches.Remove(new Point(i, j));
                        Bluestone.UpdateTorches();
                        Bluestone.UpdateBluestone();
                        break;
                    case "Delayer":
                    case "Delayer_bottom":
                    case "Delayer_left":
                    case "Delayer_top":
                    case "Delayer_right":
                    case "Delayer_bottom_unpowered":
                    case "Delayer_left_unpowered":
                    case "Delayer_top_unpowered":
                    case "Delayer_right_unpowered":
                        Bluestone.placedDelayers.Remove(new Point(i, j));
                        break;
                    default:
                        break;
                }
                MapGenerator.map[i, j] = tileList[1];
                Form.ActiveForm.Invalidate();
            }
        }
    }
}