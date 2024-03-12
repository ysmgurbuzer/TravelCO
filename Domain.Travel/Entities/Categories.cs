using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.CategoryTypes;

namespace Domain.Travel.Entities
{
    public  class Categories
    {
    
        public int Id { get; set; }
        public Category CategoryName { get; set; }    
    }
}
