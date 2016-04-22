namespace astrocalc.api.models {

    public class Locale {
        public double[] coordinates { get; set; }
        public double latitude
        {
            get
            {
                return this.coordinates[0];
            }
        }
        public double longitude
        {
            get
            {
                return this.coordinates[1];
            }
        }
    }

    public class City:Locale {
        public string title { get; set; }
       
    }
}