
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class AIRecommendation
    {
  
        public int Id { get; set; }

        public int UserId { get; set; }
        public List<string> PreferredCategories { get; set; }
        public double HomeLatitude { get; set; }
        public double HomeLongitude { get; set; }

        public double Place1Latitude { get; set; }
        public double Place1Longitude { get; set; }
        public List<string> Place1Type { get; set; }

        public double Place2Latitude { get; set; }
        public double Place2Longitude { get; set; }
        public List<string> Place2Type { get; set; }

        public double Place3Latitude { get; set; }
        public double Place3Longitude { get; set; }
        public List<string> Place3Type { get; set; }

        public double Place4Latitude { get; set; }
        public double Place4Longitude { get; set; }
        public List<string> Place4Type { get; set; }

        public double Place5Latitude { get; set; }
        public double Place5Longitude { get; set; }
        public List<string> Place5Type { get; set; }

        public double Place6Latitude { get; set; }
        public double Place6Longitude { get; set; }
        public List<string> Place6Type { get; set; }


        public double Place7Latitude { get; set; }
        public double Place7Longitude { get; set; }
        public List<string> Place7Type { get; set; }

        public double Place8Latitude { get; set; }
        public double Place8Longitude { get; set; }
        public List<string> Place8Type { get; set; }

        public double Place9Latitude { get; set; }
        public double Place9Longitude { get; set; }
        public List<string> Place9Type { get; set; }


        public double Place10Latitude { get; set; }
        public double Place10Longitude { get; set; }
        public List<string> Place10Type { get; set; }


        public double Place1Score { get; set; }
        public double Place2Score { get; set; }
        public double Place3Score { get; set; }

        public double Place4Score { get; set; }
        public double Place5Score { get; set; }
        public double Place6Score { get; set; }
        public double Place7Score { get; set; }
        public double Place8Score { get; set; }
        public double Place9Score { get; set; }
        public double Place10Score { get; set; }

        public Survey Survey { get; set; }
        public User User { get; set; }
       
        public Location Location { get; set; }

    }
}
