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
            new MapTile("Torch", 3, null, Color.Magenta),
            new MapTile("Delayer", 4, null, Color.Gray),
            new MapTile("AND_gate", 5, null, Color.Pink),
            new MapTile("OR_gate", 6, null, Color.DarkCyan)
        };

        /// <summary>
        /// Removes or adds a tile on the map depending on mouse button pressed. Left mouse button press adds a tile, while a right 
        /// mouse button press removes a tile from the map.
        /// </summary>
        /// <param name="e">used to retrieve mouse press location</param>
        public static void ProcessTileChange(MouseEventArgs e)
        {
            int i = (e.X + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointX, j = (e.Y + MapGenerator.mapOffset) / MapGenerator.sizeOfTile - 1 + FormControls.startingPointY;
            if (Form1.keyHeld == Keys.D && MapGenerator.map[i, j].ID == 1)
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
                        Bluestone.UpdateANDGates();
                        Bluestone.UpdateORGates();
                        break;
                    case 4:
                        Bluestone.placedDelayers.Add(new Point(i, j));
                        Bluestone.DetermineDelayerDirection(new Point(i, j));
                        break;
                    case 5:
                        Bluestone.placedANDgates.Add(new Point(i, j));
                        Bluestone.UpdateANDGates();
                        break;
                    case 6:
                        Bluestone.placedORgates.Add(new Point(i, j));
                        Bluestone.UpdateORGates();
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
                        Bluestone.UpdateANDGates();
                        Bluestone.UpdateORGates();
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
                    case "AND_gate":
                    case "AND_gate_powered":
                        Bluestone.placedANDgates.Remove(new Point(i, j));
                        Bluestone.UpdateBluestone();
                        break;
                    case "OR_gate":
                    case "OR_gate_powered":
                        Bluestone.placedORgates.Remove(new Point(i, j));
                        Bluestone.UpdateBluestone();
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