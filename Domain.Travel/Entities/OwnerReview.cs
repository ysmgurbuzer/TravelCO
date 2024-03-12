
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class OwnerReview
    {

        public int Id { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Owner")]
        public int HomeownerId { get; set; }
        public User User { get; set; }
        public Owner Owner { get; set; }
    }
}
