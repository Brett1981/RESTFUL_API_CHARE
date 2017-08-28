using CHARE_REST_API_v1.Models;

namespace CHARE_REST_API.Json_Object
{
    public class TripDetails
    {
        public Member Member { get; set; }
        public TripDriver TripDriver { get; set; }
        public int TripPassengerID { get; set; }
        public int PassengerID { get; set; }
        public int? TripDriverID { get; set; }

        public int DriverID { get; set; }
        public string PassengerIDs { get; set; }
        public int availableSeat { get; set; }

        public string origin { get; set; }
        public string destination { get; set; }
        public string originLatLng { get; set; }
        public string destinationLatLng { get; set; }
        public System.TimeSpan arriveTime { get; set; }
        public string femaleOnly { get; set; }
        public double cost { get; set; }
        public double distance { get; set; }
        public string days { get; set; }
        public int duration { get; set; }
        public string costStr { get; set; }
        public string durationStr { get; set; }
        public string distanceStr { get; set; }
    }
}
