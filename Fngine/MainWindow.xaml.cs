using Fngine.WorldLogic;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Fngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static MainWorld World { get; private set; }
        public static bool Paused { get; set; } = false;
        public static double WinWidth;
        public static double WinHeight;
        

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            Cursor = Cursors.None;

            // Creating the world
            World = new();

            StartGameTimer();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        public static Point GetCenter()
        {
            return new Point(WinWidth / 2, (SystemParameters.WindowCaptionHeight + WinHeight) / 2);
        }
        public static void CenterMouseInWindow(Window window)
        {
            Point windowCenter = GetCenter();

            SetCursorPos((int)(window.Left + windowCenter.X), (int)(window.Top + windowCenter.Y));
        }
        public void UpdateDebug()
        {
            // Game Timer
            decimal timercount = Convert.ToDecimal(WorldTimer.Content);
            WorldTimer.Content = timercount + Convert.ToDecimal(MainWorld.TICK_SIZE_MS / 1000);

            // Title = $"{String.Join(",", World.Player.Controller.KeysPressed.ToArray())}";

            // Player Location
            PlayerLocationX.Content = $"{Math.Round(World.ObjectData[World.Player].X)}";
            PlayerLocationY.Content = $"{Math.Round(World.ObjectData[World.Player].Y)}";
            PlayerLocationZ.Content = $"{Math.Round(World.ObjectData[World.Player].Z)}";
            PlayerLocationYaw.Content = $"{Math.Round(World.ObjectData[World.Player].Yaw)}";
            PlayerLocationPitch.Content = $"{Math.Round(World.ObjectData[World.Player].Pitch)}";
        }

        async private void StartGameTimer()
        {
            var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(MainWorld.TICK_SIZE_MS));

            while (await timer.WaitForNextTickAsync())
            {
                if (!Paused) World.ProcessWorld();
            }
        }

        public void Window_KeyHandler(object source, KeyEventArgs e)
        {
            World.Player.Controller.PlayerKeyHandler(e);
            if (e.Key == Key.Escape && e.IsDown)
            {
                Paused = !Paused;
                if (Paused)
                {
                    Title = "Fngine [PAUSED]";
                    Cursor = Cursors.Arrow;
                }

                else
                {
                    Title = "Fngine";
                    // Cursor = Cursors.None;
                }
            }
        }
        public void Window_MouseHandler(object source, MouseEventArgs e)
        {
            if (!Paused)
            {
                World.Player.Controller.PlayerLookHandler(e);

                // Center the mouse
                CenterMouseInWindow(this);
            }
            MouseX.Content = $"{(int)e.GetPosition(null).X}";
            MouseY.Content = $"{(int)e.GetPosition(null).Y}";
        }


        private void Window_SizeChangeHandler(object sender, SizeChangedEventArgs e)
        {
            WinWidth = e.NewSize.Width;
            WinHeight = e.NewSize.Height;
        }
    }
}
