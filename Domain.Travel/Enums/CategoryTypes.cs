using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Enums
{
    public class CategoryTypes
    {
        public enum Category
        {
            TreeHouses = 1,
            Tropical = 2,
            Rooms = 3,
            Extraordinary = 4,
            Design = 5,
            BeautifulViews = 6,
            CampingGear = 7,
            Islands = 8,
            NationalParks = 9,
            Beach = 10,
            StunningPools = 11,
            NorthPole = 12,
            TriangleHouses = 13,
            Caves = 14,
            Surfing = 15,
            Cabins = 16,
            Lakeside = 17,
            Camping = 18,
            TinyHouses = 19,
            New = 20,
            Skiing = 21,
            Popular = 22,
            Vineyards = 23,
            BedAndBreakfasts = 24,
            Mansions = 25,
            Rural = 27,
            EarthHouses = 28,
            Farms = 29,
            HistoricalHouses = 30,
            Castles = 31,
            SymbolicCities = 32,
            Hanoks = 33,
            Luxe = 34,
            Golf = 35,
            CycladicHouses = 36,
            TopOfTheWorld = 37,
            ProfessionalKitchens = 38,
            Domes = 39,
            CasaParticulars = 40,
            Oceanfront = 41,
            Insular = 42,
            SkiInSkiOut = 43,
            Ryokans = 44,
            Windmills = 45,
            Games = 46,
            ShepherdHuts = 47,
            GrandPianos = 48,
            Towers = 49,
            Yurts = 50,
            Barns = 51,
            Desert = 52,
            Boats = 53,
            BoatHouses = 54,
            Accessibility = 55,
            OffGrid = 56,
            Containers = 57,
            CreativeSpaces = 58,
            TrulliHouses = 59,
            Riads = 60,
            Dammusols = 61,
            Seaside = 62
        }

        public static int GetId(Category category)
        {
           
            return (int)category;
        }


    }
}
