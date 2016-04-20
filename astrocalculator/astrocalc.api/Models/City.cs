namespace astrocalc.api.models {
    public class City {
        public string Title { get; set; }
        public double[] Coordinates { get; set; }

        public double Latitude { get {
                return this.Coordinates[0];
            }}
        public double Longitude { get {
                return this.Coordinates[1];
            }
        }
    }
}