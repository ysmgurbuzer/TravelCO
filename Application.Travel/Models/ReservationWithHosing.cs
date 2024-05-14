using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Models
{
    public static class ReservationWithHosing
    {
            private static int _housingId;

            public static int HousingId
            {
                get => _housingId;
                set => _housingId = value;
            }

    }
}
