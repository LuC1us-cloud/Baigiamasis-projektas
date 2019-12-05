using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
