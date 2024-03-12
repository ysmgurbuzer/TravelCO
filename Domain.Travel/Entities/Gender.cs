using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Gender
    {
    
        public int Id { get; set; }
        public GenderType GenderType { get; set; }
    }
}
