using Domain.Travel.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class Roles
    {
   
        public int Id {  get; set; }   
        public string RoleName { get; set; }
        public List<UserRoles> userRoles { get; set; }
      
    }
}
