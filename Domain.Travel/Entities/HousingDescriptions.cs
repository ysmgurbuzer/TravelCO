
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class HousingDescriptions
    {

        public int Id { get; set; }
        public string Details { get; set; }
        public int HousingId { get; set; }
        public Housing Housing { get; set; }

    }
}
//servis ekle