
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class User
    {
   
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int GenderId { get; set; }
        public decimal? WalletAmount { get; set; } = 0.0m;
        public int RoleId { get; set; }
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();

        public List<HousingReview> HousingReviews { get; set; } = new List<HousingReview>();

        public List<OwnerReview> OwnerReviews { get; set; } = new List<OwnerReview>();
        public string Language { get; set; } = "English";
        public Gender Gender { get; set; }
        public List<UserRoles> UserRoles { get; set; }
        public List<Favorites> Favorites { get; set; }
    

    }
}
