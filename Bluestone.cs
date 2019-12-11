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
        static bool PowerCheck(Point tileLocation, string[] conditions, string direction)
        {
            foreach (var condition in conditions)
            {
                if (tileLocation.Y != 0 && MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == condition)
                {
                    if (direction == "top" || direction == "any")
                    {
                        return true;
                    }
                }
                else if (tileLocation.X != MapGenerator.sizeOfArray - 1 && MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == condition)
                {
                    if (direction == "right" || direction == "any")
                    {
                        return true;
                    }
                }
                else if (tileLocation.Y != MapGenerator.sizeOfArray - 1 && MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == condition)
                {
                    if (direction == "bottom" || direction == "any")
                    {
                        return true;
                    }
                }
                else if (tileLocation.X != 0 && MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == condition)
                {
                    if (direction == "left" || direction == "any")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Powers on bluestone that has a powered tile adjacent.
        /// </summary>
        public static void UpdateBluestone()
        {
            UnpowerBluestone();
            for (int i = 0; i < placedBluestone.Count; i++)
            {
                if (MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y].Name != "Bluestone_powered" && PowerCheck(placedBluestone[i], new string[] { "Torch", "Bluestone_powered", "Delayer_bottom", "Delayer_left", "Delayer_top", "Delayer_right" }, "any"))
                {
                    MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y] = new MapTile("Bluestone_powered", 2, null, Color.Blue);
                    i = 0;
                }
                if (placedBluestone.Count > 0 && PowerCheck(placedBluestone[0], new string[] { "Torch", "Bluestone_powered" }, "any"))
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
                if (PowerCheck(torch, new string[] { "Torch" }, "top"))
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y - 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, new string[] { "Torch" }, "right"))
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X + 1, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, new string[] { "Torch" }, "bottom"))
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y + 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (PowerCheck(torch, new string[] { "Torch" }, "left"))
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X - 1, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
            }
        }

        //FIND A WAY TO MAKE METHOD UNIVERSAL AS IT REPEATS

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
            if (PowerCheck(tileLocation, new string[] { "Torch", "Bluestone_powered"}, "top"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_bottom", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, new string[] { "Torch", "Bluestone_powered" }, "right"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_left", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, new string[] { "Torch", "Bluestone_powered" }, "bottom"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_top", 4, null, Color.LightCyan);
            }
            else if (PowerCheck(tileLocation, new string[] { "Torch", "Bluestone_powered" }, "left"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_right", 4, null, Color.LightCyan);
            }
        }

        /// <summary>
        /// Unpowers delayers that are no longer powered.
        /// </summary>
        public static void UpdateDelayers()
        {
            UnpowerDelayers();
            foreach (var delayer in placedDelayers)
            {
                if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_bottom_unpowered" && PowerCheck(delayer, new string[] { "Torch", "Bluestone_powered" }, "top"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_bottom", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_left_unpowered" && PowerCheck(delayer, new string[] { "Torch", "Bluestone_powered" }, "right"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_left", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_top_unpowered" && PowerCheck(delayer, new string[] { "Torch", "Bluestone_powered" }, "bottom"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_top", 4, null, Color.LightCyan);
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_right_unpowered" && PowerCheck(delayer, new string[] { "Torch", "Bluestone_powered" }, "left"))
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
                if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_bottom" || MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_left" || MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_top" || MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_right")
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile(MapGenerator.map[delayer.X, delayer.Y].Name + "_unpowered", 4, null, Color.Gray);
                }
            }
        }
    }
}