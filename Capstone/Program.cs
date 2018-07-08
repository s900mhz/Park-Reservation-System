using Capstone.DAL;
using Capstone.Models;
using CapstoneLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Need to fix get integer method and continue redoing the cli menu
namespace Capstone
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        static ParkReservationDAL parkReservationDAL = new ParkReservationDAL(connectionString);


        static void Main(string[] args)
        {
            // Sample Code to get a connection string from the
            // App.Config file
            // Use this so that you don't need to copy your connection string all over your code!
            while (true)
            {
                ParkReservationCLI parkReservationCLI = new ParkReservationCLI();

                //Goes to main menu where the user can select a park then outputs that selected park
                Park _userChoicePark = parkReservationCLI.GoToMainMenu();
                List<Campground> _userCampground = parkReservationDAL.GetCampgroundsInPark(_userChoicePark);

                bool loop = true;
                while (loop)
                {
                    parkReservationCLI.DisplayParkInfo(_userChoicePark);

                    int parkInfoSelection = CLIHelper.GetInteger(3);


                    switch (parkInfoSelection)
                    {
                        case 1:
                            Console.Clear();
                            //This calls the GetCampgroundsInPark method with the user selected park as the parameter
                            //Which returns a list for the DisplayParkCampgrounds method which prints that list to the screen
                            parkReservationCLI.DisplayParkCampgrounds(parkReservationDAL.GetCampgroundsInPark(_userChoicePark));
                            parkReservationCLI.DisplayParkCampgroundsCommand(parkReservationDAL.GetCampgroundsInPark(_userChoicePark));
                            int selection = CLIHelper.GetInteger(2);
                            bool menuLoop = true;
                            switch (selection)
                            {
                                case 1:
                                    while (menuLoop)
                                    {
                                        Console.Clear();
                                        parkReservationCLI.DisplayParkCampgrounds(_userCampground);
                                        menuLoop=parkReservationCLI.DisplayReservationMenu();
                                    }
                                    break;
                                case 2:
                                    break;
                            }
                            break;

                        case 2:
                            parkReservationCLI.DisplayParkCampgrounds(parkReservationDAL.GetCampgroundsInPark(_userChoicePark));

                            Console.ReadKey();
                            break;

                        case 3:
                            //This breaks off this method and returns to the main menu
                            loop = false;
                            break;
                    }
                }
            }

        }
    }
}
