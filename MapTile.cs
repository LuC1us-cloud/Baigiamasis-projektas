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

        public static void PlaceTile(MouseEventArgs e)
        {
            int i = (e.X + 10) / 20 - 1 + FormControls.startingPointX, j = (e.Y + 10) / 20 - 1 + FormControls.startingPointY;
            if (e.Button == MouseButtons.Left)
            {
                Color tileColor = new Color();
                string name = "null";
                if (MapGenerator.map[i, j].ID == 1)
                {
                    switch (FormControls.counter / 20)
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
                            Bluestone.UpdateWires();
                            break;
                        case 3:
                            tileColor = Color.Blue;
                            name = "Torch";
                            Bluestone.UpdateWires();
                            break;
                        default:
                            break;
                    }
                    MapGenerator.map[i, j] = new MapTile(name, FormControls.counter / 20, null, tileColor);
                    Form.ActiveForm.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (MapGenerator.map[i, j].ID == 2 || MapGenerator.map[i, j].ID == 3)
                {
                    placedWires.Remove(new Point(i, j));
                    Bluestone.UnpowerWires(new Point(i, j));
                }
                MapGenerator.map[i, j] = new MapTile("Grass", 1, null, Color.GreenYellow);
                Form.ActiveForm.Invalidate();
            }
        }
    }
}