using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CHARE_REST_API.Class
{
    public class Haversine
    {
        public double GetDistance(string latlng, string latlng2)
        {                        
            var arrlatlng = latlng.Split(',');
            var arrlatlng2 = latlng2.Split(',');
            double lat1, lng1, lat2, lng2;

            lat1 = Double.Parse(arrlatlng[0]);
            lng1 = Double.Parse(arrlatlng[1]);
            lat2 = Double.Parse(arrlatlng2[0]);
            lng2 = Double.Parse(arrlatlng2[1]);

            var radius = 6371; 
            var dLat = Deg2Rad(lat2 - lat1);  
            var dLon = Deg2Rad(lng2 - lng1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var calculated = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = radius * calculated; 
            return distance;
        }

        public double Deg2Rad(double deg)
        {
            return (deg * (Math.PI / 180));
        }
    }
}

