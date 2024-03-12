using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class OwnerFeatures
    {

        //2.03--eğer rolü ev sahibi olmuş ise post ile servislerini set etmeli

        public int Id { get; set; }
        public int HomeOwnerId { get; set; }
        public int ServicesId { get; set; }
        public bool Available { get; set; }
        public OwnerServiceTypes HomeOwnerServicesType { get; set; }
        public Owner Homeowner { get; set; }

    }
}
