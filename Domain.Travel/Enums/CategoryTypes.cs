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
            LogCabins = 2,
            StrawBaleHouses = 3,
            EarthShelteredHomes = 4,
            CobHouses = 5,
            BambooHouses = 6,
            CordwoodHouses = 7,
            AdobeHouses = 8,
            TinyHouses = 9,
            EcoVillages = 10,
            SustainableFarms = 11,
            PermacultureRetreats = 12,
            OffGridHomes = 13,
            NaturalMaterialHouses = 14,
            PassiveSolarHouses = 15,
            GreenRoofs = 16,
            SolarPoweredHouses = 17,
            GeodesicDomes = 18,
            FloatingHouses = 19,
            Houseboats = 20,
            StrawbaleCabins = 21,
            TimberFrameHouses = 22,
            SelfSufficientHomes = 23,
            EarthbagHouses = 24,
            RecycledMaterialHouses = 25,
            Homesteads = 26,
            WildernessRetreats = 27,
            EcoLodges = 28,
            AgriTourism = 29,
            ForestCabins = 30,
            GreenCabins = 31,
            EcoFriendlyVillas = 32,
            SustainableCottages = 33,
            RenewableEnergyHomes = 34,
            SustainableCommunities = 35,
            EcoResorts = 36,
            WildCamping = 37,
            EcologicalSanctuaries = 38,
            AlpineCabins = 39,
            EcoRetreats = 40,
            WildernessCabins = 41,
            RainforestRetreats = 42,
            SolarHouses = 43,
            BioArchitecture = 44,
            StrawbaleRetreats = 45,
            NaturalBuilding = 46,
            PermacultureFarms = 47,
            GreenBuilding = 48,
            EcoCabins = 49,
            SustainableLiving = 50,
            ForestHuts = 51,
            EcoCamps = 52,
            EarthshipHomes = 53,
            SustainableTinyHomes = 54,
            EcoFriendlyCabins = 55,
            EcoVillageHomes = 56,
            ForestRetreats = 57,
            RenewableHouses = 58,
            EcoCottages = 59,
            SolarCabins = 60,
            GeothermalHouses = 61,
            SustainableGetaways = 62,
            ForestHideaways = 63
        }

        public static int GetId(Category category)
        {
           
            return (int)category;
        }
        private static readonly Dictionary<int, string> CategoryNames = new Dictionary<int, string>();

       

        public static string GetName(int categoryId)
        {
            if (CategoryNames.ContainsKey(categoryId))
            {
                return CategoryNames[categoryId];
            }
            else
            {
                return null; 
            }
        }
    }
}
