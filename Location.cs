using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movable_2dmap
{
    class Location
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;

        static Location current = null;
        static Location start = new Location { X = 1, Y = 2 };
        static Location target = new Location { X = 2, Y = 5 };
        static List<Location> openList = new List<Location>();
        static List<Location> closedList = new List<Location>();
        static int g = 0;
    }
}
