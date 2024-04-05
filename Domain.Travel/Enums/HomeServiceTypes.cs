using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Enums
{
    public class HomeServiceTypesClass
    {
        public enum HomeServiceTypes
        {
            HairDryer,
            CleaningProducts,
            HotWater,
            TowelsAndToiletPaper,
            Hangers,
            BedSheets,
            CottonBedLinens,
            ExtraPillowsAndBlankets,
            BlackoutCurtains,
            Iron,
            Safe,
            ClothingStorage,
            Entertainment,
            HDTVAndNetflix,
            ChildrensBooksAndToys,

            CentralAC,
            Heating,
            HomeSecurity,
            SmokeDetector,
            CarbonMonoxideAlarm,
            FireExtinguisher,
            Wifi,
            PrivateWorkspace,
            KitchenAndDining,
            GuestsCanCook,
            Refrigerator,
            Microwave,
            BasicCookingSupplies,
            PotsAndPans,

            Freezer,
            Oven,
            ElectricKettle,
            CoffeeMaker,
            WineGlasses,
            Waterfront,
            PrivateBeachAccess,
            GuestsCanEnjoyNearbyBeach,
            FacilityAccess,
            GuestsCanUseNearbyResortFacilities,
            Outdoor,
            PrivateDeckOrBalcony,
            FullyFencedPrivateBackyard,
            OutdoorSpaceUsuallyLawnCovered,
            GardenFurniture,
            OutdoorDiningArea,
            CharcoalWoodBarbecue,
            BeachEssentials,
            HalfBoard,
            Fullpension,
            ParkingAndFacilities,
            ViewRooms,
            Elevator,
            Services,
            LuggageDropoffAllowed,
            EarlyOrLateCheckin,
            LongTermStaysAllowed,
            AllowedForStays28DaysOrMore,
            SelfCheckIn,


        }

        public static List<string> GetAllServiceTypes()
        {
            return Enum.GetNames(typeof(HomeServiceTypes)).ToList();
        }
    }
   
   
       
    
}
