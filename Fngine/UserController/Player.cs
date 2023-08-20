using Fngine.WorldLogic;

namespace Fngine.UserController
{
    public class Player : WorldObject
    {
        public static double SPEED = 4;
        public PlayerControl Controller { get; }
        public Player() {
            Controller = new();
        }

    }
}
