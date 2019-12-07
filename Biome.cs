using System.Drawing;

namespace movable_2dmap
{
    public class Biome
    {
        public string Name { get; }
        public Point Location { get; }
        public int Size { get; set; }
        public Biome(string name, Point location)
        {
            Name = name;
            Location = location;
        }
    }
}