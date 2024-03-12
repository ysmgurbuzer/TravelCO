
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Favorites
    {
    
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Housing")]
        public int HousingId { get; set; }
        public Housing FavoriteHousings { get; set; }
        public User Users { get; set; }

    }
}
