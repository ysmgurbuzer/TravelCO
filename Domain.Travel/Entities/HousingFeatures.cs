using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class HousingFeatures
    {

        public int Id {  get; set; }
        public int HousingId { get; set; }
        public HomeServiceTypes Name { get; set; }
        public bool Available {  get; set; }     
        public Housing Housing { get; set; }

    }
}
