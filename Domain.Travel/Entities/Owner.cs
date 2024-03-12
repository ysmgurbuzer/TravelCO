
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Owner
    {
  
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ResponseTime {  get; set; }
        public HomeownerTitle Title { get; set; }
        public User User { get; set; }
        public List<Housing> Housing { get; set; }
        public List<OwnerFeatures> OwnerFeatures { get; set; }

        public List<OwnerReview> OwnerReviews { get; set; } = new List<OwnerReview>();
    }
    public enum HomeownerTitle
    {
        New=1,
        Ambitious=2,
        Praiseworthy=3,
        Super=4,

    }

}
