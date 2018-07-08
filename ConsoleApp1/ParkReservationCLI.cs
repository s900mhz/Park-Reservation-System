using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Capstone
{    
    class ParkReservationCLI
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

        public void GoToMainMenu()
        {
            //Greeting
            Console.WriteLine("Select a Park for Further Details");
            //List of Parks
                //Leads into the ParkCampgroundMenu();
            //Q to quit
        }
        public void GoToParkCampgroundMenu()
        {
            //Name of the campground
            //Details on the campsites inside the campground

            //Options: Search for Available Reservations
            //Return to previous
            //MainMenu
        }
        public void GotoCampgroundReservationMenu()
        {
            //Display Campsites
            //Which campground?
            //What is the arrival date?
            //What is the departure date?
        }
    }
}
