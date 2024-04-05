
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class HousingReview
    {

        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }


        [ForeignKey("Housings")]
        public int HousingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public Housing Housing { get; set; }
    }
}
//servis eklenecek