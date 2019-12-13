using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace movable_2dmap
{
    public class MapTile
    {
        public string Name { get; }
        public int ID { get; set; }
        public MapTile(string name, int id)
        {
            Name = name;
            ID = id;
        }

        public static List<MapTile> tileList = new List<MapTile>{
            new MapTile("Null", 0),
            new MapTile("Grass", 1),
            new MapTile("Food", 2)
        };
        public  override string ToString()
        {
            string temp = Name + " " + ID.ToString();
            return temp;
        }
    }
}