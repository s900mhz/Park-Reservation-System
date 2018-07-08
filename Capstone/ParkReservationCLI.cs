using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Capstone.DAL;
using Capstone.Models;
using CapstoneLib;
namespace Capstone
   //Format the console text, Including headers and padding.
   //Check all inputs to make sure it's unbreakable.
{    
   public class ParkReservationCLI
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        private Park _UserChoicePark;
        private List <Campground> _userChoiceCampgroundList;
        private Campground _userChoiceCampground;
        static ParkReservationDAL parkReservationDAL = new ParkReservationDAL(connectionString);
        private CampSite _UserChoiceCampsite;
        static DateTime arrivalDate;
        static DateTime departureDate;
        static int amtOfDays;


        


        public Park GoToMainMenu()
        {
            Console.Clear();
            //Greeting
            Console.WriteLine("Welcome to the Park Reservation System");
            Console.WriteLine("Please Select a Park for Further Details");
            

            //List of Parks
            //ParkReservationDAL parkReservationDAL = new ParkReservationDAL(connectionString);
            List<Park> listOfParks = parkReservationDAL.GetAllParks();
            //Adding the list of parks into a dictionary that the key (j) will start at 1
            Dictionary<int, Park> dicOfParks = new Dictionary<int, Park>();
            int j = 1;
            for (int i = 0; i < listOfParks.Count; i++)
            {
                dicOfParks[j] = listOfParks[i];
                j++;
            }
            //The count is so we can display the number needed to select that park
            int count = 1;
            foreach (var park in listOfParks)
            {
                Console.WriteLine($"{count++}) {park.Name}");
            }
            Console.WriteLine("Please Select a Park");
            int parkSelectionInt = CLIHelper.GetInteger(dicOfParks.Count);

            
            _UserChoicePark = dicOfParks[parkSelectionInt];
            return _UserChoicePark;
                           
        }        
        public bool DisplayReservationMenu()
        {
            //ParkReservationDAL parkReservationDAL = new ParkReservationDAL(connectionString);
            Console.WriteLine("Which Campground? (Press 0 to Cancel)");
            int selection = CLIHelper.GetInteger();
            if (selection == 0)
            {
                return false;
            }
            else
            {
                _userChoiceCampground = _userChoiceCampgroundList[selection - 1];

                Console.WriteLine();
                Console.WriteLine("What is the Arrival Date? mm/dd/yyyy");
                arrivalDate = CLIHelper.GetDate(Console.ReadLine());
                Console.WriteLine("What is the Departure Date? mm/dd/yyyy");
                departureDate = CLIHelper.GetDate(Console.ReadLine());
                //List<CampSite> campSites = parkReservationDAL.GetCampSitesInCampGround(userCampground);
                List<CampSite> reservations = parkReservationDAL.GetAvailableReservations(_userChoiceCampground, arrivalDate, departureDate);
                amtOfDays = (departureDate - arrivalDate).Days;
                DisplayCampsites(reservations);
                DisplayReservationCommands(reservations);
                return true;
            }
            
        }
        public void DisplayParkInfo(Park park)
        {
            Console.Clear();
            Console.WriteLine($"{park.Name} National Park");
            Console.WriteLine($"Location: \t {park.Location}");
            Console.WriteLine($"Established: \t {park.EstablishDate}");
            Console.WriteLine($"Area: \t {park.Area} sq km");
            Console.WriteLine($"Annual Visitor \t {park.Visitors}");
            DisplayParkInfoCommand(park);
        }
        public void DisplayParkInfoCommand(Park park)
        {
            Console.WriteLine();
            Console.WriteLine("Please Select a Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Available Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            ParkReservationDAL parkReservationDAL = new ParkReservationDAL(connectionString);
            
        }

        public void DisplayReservationCommands(List<CampSite> availableSites)
        {
            //This builds a dynamic menu
            Dictionary<int, CampSite> dicOfCamps = new Dictionary<int, CampSite>();
            int j = 1;
            for (int i = 0; i < availableSites.Count; i++)
            {
                dicOfCamps[j] = availableSites[i];
            }
            Console.Write("Which site should be reserved (enter 0 to cancel)");
            int reservationChoice = CLIHelper.GetInteger();
            string reservationName = CLIHelper.GetString("What name should the reservation be made under?");
            
            _UserChoiceCampsite = dicOfCamps[reservationChoice];
            int reservationid = parkReservationDAL.WriteReservation(_UserChoiceCampsite, reservationName, arrivalDate, departureDate);

            Console.WriteLine($"The reservation has been made and the confirmation id is {reservationid}");
            Console.ReadKey();
        }

        public void DisplayParkCampgrounds(List<Campground> listofcampgrounds)
        {
            
            int count = 1;
            string[] header = new string[] { string.Format("{0,-15}", "Name"), string.Format("{0,-15}", "Open (Month)"), string.Format("{0,-15}", "Close (Month)"), string.Format("{0,-15}", "Daily Fee") };
            foreach (var item in header)
            {
                Console.Write(item);
            }
            foreach (var campground in listofcampgrounds)
            {
               
                Console.WriteLine();
                Console.WriteLine("{0,-1}{1,-15}{2,-15}{3,-15}{4,-15}", count++ + ")", campground.Name, campground.OpenFrom, campground.OpenTo, campground.DailyFee.ToString("c"));
            }
        }    
        public void DisplayParkCampgroundsCommand(List<Campground> campGrounds)
        {
            _userChoiceCampgroundList = campGrounds;
            
            Console.WriteLine();
            Console.WriteLine("Please Select A Command");
            Console.WriteLine();
            Console.WriteLine("1) Search For Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            
            //int selection = CLIHelper.GetInteger(2);
            //switch (selection)
            //{
            //    case 1:
            //        Console.Clear();
            //        DisplayParkCampgrounds(campGrounds);
            //        DisplayReservationMenu();
            //        break;
            //    default:
            //        break;
            //}
        }
        public void DisplayCampsites(List<CampSite> campSites)
        {
            decimal stayFee = 0;
            int count = 1;
            Console.WriteLine("{0,-16}{1,-16}{2,-16}{3,-16}{4,-16}{5,-16}", "Site No.", "Max Occup.", "Accessible?", "Max RV Length", "Utility", "Cost");
            foreach (var campsite in campSites)
            {
                stayFee = (amtOfDays * _userChoiceCampground.DailyFee);
                Console.WriteLine("{0,-1}{1,-16}{2,-16}{3,-16}{4,-16}{5,-16}{6,-17}", count++ +")", campsite.SiteNumber, campsite.MaxOccupancy, campsite.Accesible, campsite.MaxRvLength, campsite.Utilities, stayFee.ToString("c"));
            }
        }
       
 
    }
}
