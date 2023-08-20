using Fngine.DataWrappers;
using Fngine.WorldLogic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Fngine.UserController
{
	public class PlayerControl
	{
		public List<Key> KeysPressed { get; } = new();
		private Dictionary<Key, string> Controls = new()
			{
			{ Key.W, "+X" },
			{ Key.S, "-X" },
			{ Key.A, "+Z" },
			{ Key.D, "-Z" },
			{ Key.Space, "+Y" },
			{ Key.LeftShift, "-Y" }
		};
		public PlayerControl()
		{

		}
		public void PlayerKeyHandler(KeyEventArgs e)
		{
			bool isdown = e.IsDown;
			if (isdown && !KeysPressed.Contains(e.Key)) KeysPressed.Add(e.Key);
			else if (!isdown) KeysPressed.Remove(e.Key);
		}
		public void PlayerMoveHandler()
		{
			Location PlayerLoc = MainWindow.World.ObjectData[MainWindow.World.Player];
			double Distance = Player.SPEED * (MainWorld.TICK_SIZE_MS / 1000);
            if (KeysPressed.Contains(Key.W))
            {
                PlayerLoc.X += Distance;
            }
            if (KeysPressed.Contains(Key.S))
            {
                PlayerLoc.X -= Distance;
            }
            if (KeysPressed.Contains(Key.A))
            {
                PlayerLoc.Z += Distance;
            }
            if (KeysPressed.Contains(Key.D))
            {
                PlayerLoc.Z -= Distance;
            }
            if (KeysPressed.Contains(Key.Space))
            {
                PlayerLoc.Y += Distance;
            }
            if (KeysPressed.Contains(Key.LeftShift))
            {
                PlayerLoc.Y -= Distance;
            }
            
            MainWindow.World.UpdateObject(MainWindow.World.Player, PlayerLoc);
        }
        public void PlayerLookHandler(MouseEventArgs e)
        {
            double sensitivity = 0.08;

            var titleHeight = SystemParameters.WindowCaptionHeight + 2*SystemParameters.ResizeFrameHorizontalBorderHeight;
            var verticalBorderWidth = 2*SystemParameters.ResizeFrameVerticalBorderWidth;

            Point WinCenter = MainWindow.GetCenter();
            Point MousePos = e.GetPosition(MainWindow.Instance);
            MousePos.X += verticalBorderWidth;
            MousePos.Y += titleHeight;

            Console.WriteLine($"{MousePos.X} {MousePos.Y}");
            double OffsetX = (int)(WinCenter.X - MousePos.X);
            double OffsetY = (int)(WinCenter.Y - MousePos.Y);

            OffsetX *= sensitivity;
            OffsetY *= sensitivity;

            Location PlayerLoc = MainWindow.World.ObjectData[MainWindow.World.Player];

            // Adjust the Yaw (horizontal rotation) within the range of 0 to 360 degrees
            PlayerLoc.Yaw = (PlayerLoc.Yaw + OffsetX) % 360;
            if (PlayerLoc.Yaw < 0)
                PlayerLoc.Yaw += 360;

            // Adjust the Pitch (vertical rotation) within the range of -90 to 90 degrees
            PlayerLoc.Pitch = PlayerLoc.Pitch + OffsetY;
            if (PlayerLoc.Pitch < -90) PlayerLoc.Pitch = -90;
            if (PlayerLoc.Pitch > 90) PlayerLoc.Pitch = 90;

            MainWindow.World.UpdateObject(MainWindow.World.Player, PlayerLoc);
        }
	}
}
