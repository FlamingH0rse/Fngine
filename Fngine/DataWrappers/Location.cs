namespace Fngine.DataWrappers
{
    public class Location
    {
        public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }
		public double Yaw { get; set; }
		public double Pitch { get; set; }
        public Location(double X, double Y, double Z, double Yaw, double Pitch) {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.Yaw = Yaw;
            this.Pitch = Pitch;
        }
    }
}
