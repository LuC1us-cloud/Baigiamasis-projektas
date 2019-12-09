﻿using System.Collections.Generic;
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
            new MapTile("Delayer", 4, null, Color.Gray)
        };

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
                        Bluestone.DetermineDirection(new Point(i, j));
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