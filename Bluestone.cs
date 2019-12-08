using System.Drawing;

namespace movable_2dmap
{
    class Bluestone
    {
        static bool PowerCheck(Point tileLocation)
        {
            if (tileLocation.Y != 0 && (MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y - 1].Name == "Bluestone_powered"))
            {
                return true;
            }
            else if (tileLocation.X != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X + 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                return true;
            }
            else if (tileLocation.Y != MapGenerator.sizeOfArray - 1 && (MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Torch" || MapGenerator.map[tileLocation.X, tileLocation.Y + 1].Name == "Bluestone_powered"))
            {
                return true;
            }
            else if (tileLocation.X != 0 && (MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Torch" || MapGenerator.map[tileLocation.X - 1, tileLocation.Y].Name == "Bluestone_powered"))
            {
                return true;
            }
            return false;
        }

        public static void UpdateBluestone()
        {
            UnpowerBluestone();
            for (int i = 0; i < MapTile.placedBluestone.Count; i++)
            {
                if (MapGenerator.map[MapTile.placedBluestone[i].X, MapTile.placedBluestone[i].Y].Name != "Bluestone_powered" && PowerCheck(MapTile.placedBluestone[i]))
                {
                    MapGenerator.map[MapTile.placedBluestone[i].X, MapTile.placedBluestone[i].Y] = new MapTile("Bluestone_powered", 2, null, Color.Blue);
                    i = 0;
                }
            }
        }

        static void UnpowerBluestone()
        {
            foreach (var bluestone in MapTile.placedBluestone)
            {
                MapGenerator.map[bluestone.X, bluestone.Y] = MapTile.tileList[2];
            }
        }

        public static void UpdateTorches()
        {
            RepowerTorches();
            foreach (var torch in MapTile.placedTorches)
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

        static void RepowerTorches()
        {
            foreach (var torch in MapTile.placedTorches)
            {
                MapGenerator.map[torch.X, torch.Y] = MapTile.tileList[3];
            }
        }
    }
}