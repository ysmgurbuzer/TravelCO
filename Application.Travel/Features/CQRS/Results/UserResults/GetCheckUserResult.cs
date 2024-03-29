
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Results.UserResults
{
    public class GetCheckUserResult
    {
        public int Id {  get; set; }   
        public string Username { get; set; } 
        public int RoleId {  get; set; }   
        public bool IsExist {  get; set; }
    }
}
