using System.Collections.Generic;
using System.Drawing;

namespace movable_2dmap
{
    class Bluestone
    {
        public static List<Point> placedBluestone = new List<Point>();
        public static List<Point> placedTorches = new List<Point>();
        public static List<Point> placedDelayers = new List<Point>();

        /// <summary>
        /// Checks in four directions if there is a tile with power and returns true if found, returns false otherwise.
        /// </summary>
        /// <param name="tileLocation"></param>
        /// <returns></returns>
        static string PowerCheck(Point tileLocation, string condition)
        {
            if (tileLocation.Y != 0 && MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == condition )
            {
                return "top";
            }
            else if (tileLocation.X != MapGenerator.sizeOfArray - 1 && MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == condition)
            {
                return "right";
            }
            else if (tileLocation.Y != MapGenerator.sizeOfArray - 1 && MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == condition)
            {
                return "bottom";
            }
            else if (tileLocation.X != 0 && MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == condition)
            {
                return "left";
            }
            return "null";
        }

        /// <summary>
        /// Powers on bluestone that has a powered tile adjacent.
        /// </summary>
        public static void UpdateBluestone()
        {
            UnpowerBluestone();
            for (int i = 0; i < placedBluestone.Count; i++)
            {
                if (MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y].Name != "Bluestone_powered" && (PowerCheck(placedBluestone[i], "Torch") != "null" || PowerCheck(placedBluestone[i], "Bluestone_powered") != "null"))
                {
                    MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y] = new MapTile("Bluestone_powered", 2, null, Color.Blue);
                    i = 0;
                }
                if (placedBluestone.Count > 0 && (PowerCheck(placedBluestone[0], "Torch") != "null" || PowerCheck(placedBluestone[0], "Bluestone_powered") != "null"))
                {
                    MapGenerator.map[placedBluestone[0].X, placedBluestone[0].Y] = new MapTile("Bluestone_powered", 2, null, Color.Blue);
                }
            }
        }

        /// <summary>
        /// Unpowers all bluestone.
        /// </summary>
        static void UnpowerBluestone()
        {
            foreach (var bluestone in placedBluestone)
            {
                MapGenerator.map[bluestone.X, bluestone.Y] = MapTile.tileList[2];
            }
        }

        /// <summary>
        /// Unpowers torches that have other torches adjacent.
        /// </summary>
        public static void UpdateTorches()
        {
            RepowerTorches();
            foreach (var torch in placedTorches)
            {
                if (PowerCheck(torch, "Torch") == "top")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y - 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, "Torch") == "right")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X + 1, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, "Torch") == "bottom")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y + 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, "Torch") == "left")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X - 1, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
            }
        }

        /// <summary>
        /// Powers on all torches.
        /// </summary>
        static void RepowerTorches()
        {
            foreach (var torch in placedTorches)
            {
                MapGenerator.map[torch.X, torch.Y] = MapTile.tileList[3];
            }
        }

        /// <summary>
        /// Checks from which direction delayer is powered and sets its facing direction opposite. Can only take one input, priority order of checking: top, right, bottom, left. 
        /// </summary>
        /// <param name="tileLocation"></param>
        public static void DetermineDelayerDirection(Point tileLocation)
        {
            if (PowerCheck(tileLocation, "Torch") == "top" || PowerCheck(tileLocation, "Bluestone_powered") == "top")
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_bottom", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, "Torch") == "right" || PowerCheck(tileLocation, "Bluestone_powered") == "right")
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_left", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, "Torch") == "bottom" || PowerCheck(tileLocation, "Bluestone_powered") == "bottom")
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_top", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, "Torch") == "left" || PowerCheck(tileLocation, "Bluestone_powered") == "left")
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_right", 4, null, Color.LightCyan);
            }
        }

        //PROBLEM: when repowering delayer, it does not get repowered

        /// <summary>
        /// Unpowers delayers that are no longer powered.
        /// </summary>
        public static void UpdateDelayers()
        {
            UnpowerDelayers();
            foreach (var delayer in placedDelayers)
            {
                if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_bottom_unpowered" && (PowerCheck(delayer, "Torch") == "top" || PowerCheck(delayer, "Bluestone_powered") == "top"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_bottom", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_left_unpowered" && (PowerCheck(delayer, "Torch") == "right" || PowerCheck(delayer, "Bluestone_powered") == "right"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_left", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_top_unpowered" && (PowerCheck(delayer, "Torch") == "bottom" || PowerCheck(delayer, "Bluestone_powered") == "bottom"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_top", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_right_unpowered" && (PowerCheck(delayer, "Torch") == "left" || PowerCheck(delayer, "Bluestone_powered") == "left"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_right", 4, null, Color.LightCyan);
                }
            }
        }

        /// <summary>
        /// Unpowers all delayers.
        /// </summary>
        static void UnpowerDelayers()
        {
            foreach (var delayer in placedDelayers)
            {
                MapGenerator.map[delayer.X, delayer.Y] = new MapTile(MapGenerator.map[delayer.X, delayer.Y].Name + "_unpowered", 4, null, Color.Gray);
            }
        }
    }
}