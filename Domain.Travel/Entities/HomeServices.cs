﻿using Domain.Travel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Travel.Enums.HomeServiceTypesClass;

namespace Domain.Travel.Entities
{
    public class HomeServices
    {

        public int Id { get; set; }
        public HomeServiceTypes Name { get; set; }

      
    }
}
