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
        static bool PowerCheck(Point tileLocation)
        {
            if (tileLocation.Y != 0 && (MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Bluestone_powered" || MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Delayer_bottom"))
            {
                return true;
            }
            else if (tileLocation.X != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                return true;
            }
            else if (tileLocation.Y != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Bluestone_powered" || MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Delayer_top"))
            {
                return true;
            }
            else if (tileLocation.X != 0 && (MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                return true;
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
                if (MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y].Name != "Bluestone_powered" && PowerCheck(placedBluestone[i]))
                {
                    MapGenerator.map[placedBluestone[i].X, placedBluestone[i].Y] = new MapTile("Bluestone_powered", 2, null, Color.Blue);
                    i = 0;
                }
                if (placedBluestone.Count > 0 && PowerCheck(placedBluestone[0]))
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
                if (torch.Y != 0 && MapGenerator.map[torch.X, torch.Y - 1].Name == "Torch")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y - 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (torch.X != MapGenerator.sizeOfArray - 1 && MapGenerator.map[torch.X + 1, torch.Y].Name == "Torch")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X + 1, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (torch.Y != MapGenerator.sizeOfArray - 1 && MapGenerator.map[torch.X, torch.Y + 1].Name == "Torch")
                {
                    MapGenerator.map[torch.X, torch.Y] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                    MapGenerator.map[torch.X, torch.Y + 1] = new MapTile("Torch_unpowered", 3, null, Color.DarkMagenta);
                }
                if (torch.X != 0 && MapGenerator.map[torch.X - 1, torch.Y].Name == "Torch")
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
        public static void DetermineDirection(Point tileLocation)
        {
            if (tileLocation.Y != 0 && (MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Bluestone_powered"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_bottom", 4, null, Color.AntiqueWhite);
            }
            else if (tileLocation.X != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_left", 4, null, Color.AntiqueWhite);
            }
            else if (tileLocation.Y != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Bluestone_powered"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_top", 4, null, Color.AntiqueWhite);
            }
            else if (tileLocation.X != 0 && (MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                MapGenerator.map[tileLocation.X, tileLocation.Y] = new MapTile("Delayer_right", 4, null, Color.AntiqueWhite);
            }
        }

        public static void UpdateDelayers()
        {
            foreach (var delayer in placedDelayers)
            {
                if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_bottom" && MapGenerator.map[delayer.X, delayer.Y - 1].Name != "Torch" && MapGenerator.map[delayer.X, delayer.Y - 1].Name != "Bluestone_powered")
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_bottom_unpowered", 4, null, Color.Gray);
                    UpdateBluestone();
                    Form1.ActiveForm.Invalidate();
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_bottom_unpowered" && (MapGenerator.map[delayer.X, delayer.Y - 1].Name == "Torch" || MapGenerator.map[delayer.X, delayer.Y - 1].Name != "Bluestone_powered"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_bottom", 4, null, Color.AntiqueWhite);
                    UpdateBluestone();
                    Form1.ActiveForm.Invalidate();
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_top" && (MapGenerator.map[delayer.X, delayer.Y + 1].Name != "Torch" && MapGenerator.map[delayer.X, delayer.Y + 1].Name != "Bluestone_powered"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_top_unpowered", 4, null, Color.Gray);
                    UpdateBluestone();
                    Form1.ActiveForm.Invalidate();
                }
                else if (MapGenerator.map[delayer.X, delayer.Y].Name == "Delayer_top_unpowered" && (MapGenerator.map[delayer.X, delayer.Y + 1].Name == "Torch" || MapGenerator.map[delayer.X, delayer.Y + 1].Name != "Bluestone_powered"))
                {
                    MapGenerator.map[delayer.X, delayer.Y] = new MapTile("Delayer_top", 4, null, Color.AntiqueWhite);
                    UpdateBluestone();
                    Form1.ActiveForm.Invalidate();
                }
            }
        }
    }
}