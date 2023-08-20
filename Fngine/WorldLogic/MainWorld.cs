using Fngine.DataWrappers;
using Fngine.Geometry;
using Fngine.UserController;
using System.Collections.Generic;

namespace Fngine.WorldLogic
{
    public class MainWorld
    {
        private MainWindow Window = MainWindow.Instance;

        // 1/20th of a second
        public static double TICK_SIZE_MS = 50d;

        public Player Player { get; private set; }
        public Dictionary<WorldObject, Location> ObjectData { get; } = new();
        public MainWorld()
        {
            // Creating the player
            Player = new();
            CreateObject(Player, new Location(0, 0, 0, 0, 0));
            CreateObject(new Point(), new Location(15, 0, 0, 0, 0));
        }

        public void CreateObject(WorldObject worldobject, Location location)
        {
            ObjectData.Add(worldobject, location);
        }
        public void UpdateObject(WorldObject worldobject, Location location) {
            if (ObjectData.ContainsKey(worldobject))
            {
                ObjectData[worldobject] = location;
            }
        }
        public void ProcessWorld()
        {
            Window.UpdateDebug();
            Player.Controller.PlayerMoveHandler();
        }
    }
}
