using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Bluestone
    {
        public static void UpdateWires()
        {
            for (int i = 0; i < MapTile.placedWires.Count; i++)
            {
                if (MapTile.placedWires[i].Y != 0 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y - 1].ID == 3 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y].ID != 3)
                {
                    MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y] = new MapTile("Bluestone", 3, null, Color.Blue);
                    i = 0;
                }
                if (MapTile.placedWires[i].X != MapGenerator.SizeOfArray - 1 && MapGenerator.map[MapTile.placedWires[i].X + 1, MapTile.placedWires[i].Y].ID == 3 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y].ID != 3)
                {
                    MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y] = new MapTile("Bluestone", 3, null, Color.Blue);
                    i = 0;
                }
                if (MapTile.placedWires[i].Y != MapGenerator.SizeOfArray - 1 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y + 1].ID == 3 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y].ID != 3)
                {
                    MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y] = new MapTile("Bluestone", 3, null, Color.Blue);
                    i = 0;
                }
                if (MapTile.placedWires[i].X != 0 && MapGenerator.map[MapTile.placedWires[i].X - 1, MapTile.placedWires[i].Y].ID == 3 && MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y].ID != 3)
                {
                    MapGenerator.map[MapTile.placedWires[i].X, MapTile.placedWires[i].Y] = new MapTile("Bluestone", 3, null, Color.Blue);
                    i = 0;
                }
            }
        }

        public static void UnpowerWires(Point powerCutLocation)
        {
            List<Point> unpoweredWires = new List<Point>();
            if (MapGenerator.map[powerCutLocation.X, powerCutLocation.Y].ID == 3)
            {
                if (MapGenerator.map[powerCutLocation.X, powerCutLocation.Y - 1].ID == 3 && MapGenerator.map[powerCutLocation.X, powerCutLocation.Y - 1].Name == "Bluestone")
                {
                    MapGenerator.map[powerCutLocation.X, powerCutLocation.Y - 1] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                    unpoweredWires.Add(new Point(powerCutLocation.X, powerCutLocation.Y - 1));
                }
                if (MapGenerator.map[powerCutLocation.X + 1, powerCutLocation.Y].ID == 3 && MapGenerator.map[powerCutLocation.X + 1, powerCutLocation.Y].Name == "Bluestone")
                {
                    MapGenerator.map[powerCutLocation.X + 1, powerCutLocation.Y] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                    unpoweredWires.Add(new Point(powerCutLocation.X + 1, powerCutLocation.Y));
                }
                if (MapGenerator.map[powerCutLocation.X, powerCutLocation.Y + 1].ID == 3 && MapGenerator.map[powerCutLocation.X, powerCutLocation.Y + 1].Name == "Bluestone")
                {
                    MapGenerator.map[powerCutLocation.X, powerCutLocation.Y + 1] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                    unpoweredWires.Add(new Point(powerCutLocation.X, powerCutLocation.Y + 1));
                }
                if (MapGenerator.map[powerCutLocation.X - 1, powerCutLocation.Y].ID == 3 && MapGenerator.map[powerCutLocation.X - 1, powerCutLocation.Y].Name == "Bluestone")
                {
                    MapGenerator.map[powerCutLocation.X - 1, powerCutLocation.Y] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                    unpoweredWires.Add(new Point(powerCutLocation.X - 1, powerCutLocation.Y));
                }
                for (int i = 0; i < unpoweredWires.Count; i++)
                {
                    for (int j = 0; j < MapTile.placedWires.Count; j++)
                    {
                        if (MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y - 1].ID == 3 && MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y - 1].Name == "Bluestone")
                        {
                            unpoweredWires.Add(new Point(unpoweredWires[i].X, unpoweredWires[i].Y - 1));
                            MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y - 1] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                        }
                        if (MapGenerator.map[unpoweredWires[i].X + 1, unpoweredWires[i].Y].ID == 3 && MapGenerator.map[unpoweredWires[i].X + 1, unpoweredWires[i].Y].Name == "Bluestone")
                        {
                            unpoweredWires.Add(new Point(unpoweredWires[i].X + 1, unpoweredWires[i].Y));
                            MapGenerator.map[unpoweredWires[i].X + 1, unpoweredWires[i].Y] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                        }
                        if (MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y + 1].ID == 3 && MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y + 1].Name == "Bluestone")
                        {
                            unpoweredWires.Add(new Point(unpoweredWires[i].X, unpoweredWires[i].Y + 1));
                            MapGenerator.map[unpoweredWires[i].X, unpoweredWires[i].Y + 1] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                        }
                        if (MapGenerator.map[unpoweredWires[i].X - 1, unpoweredWires[i].Y].ID == 3 && MapGenerator.map[unpoweredWires[i].X - 1, unpoweredWires[i].Y].Name == "Bluestone")
                        {
                            unpoweredWires.Add(new Point(unpoweredWires[i].X - 1, unpoweredWires[i].Y));
                            MapGenerator.map[unpoweredWires[i].X - 1, unpoweredWires[i].Y] = new MapTile("Bluestone", 2, null, Color.DarkBlue);
                        }
                    }
                }
            }
        }
    }
}