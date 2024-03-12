using Application.Travel.Profiles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel._Helpers
{
    public static class ProfileHelpers
    {
        public static List<Profile> GetProfiles()
        {

            return new List<Profile>
            {
                new HousingProfile(),
                new FavoritesProfile(),
                new ReservationProfile(),

            };




        }



    }
}
