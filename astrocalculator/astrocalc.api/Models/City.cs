namespace astrocalc.api.models {

    public class Locale {
        public double[] Coordinates { get; set; }

        public double Latitude
        {
            get
            {
                return this.Coordinates[0];
            }
        }
        public double Longitude
        {
            get
            {
                return this.Coordinates[1];
            }
        }
    }

    public class City:Locale {
        public string Title { get; set; }
       
    }
}