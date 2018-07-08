using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ParkReservationDAL
    {
        private string _connectionstring;

        public ParkReservationDAL(string Connection)
        {
            _connectionstring = Connection;
        }

        public List<Campground> GetCampgroundsInPark(Park park)
        {
            List<Campground> campgrounds = new List<Campground>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                const string sqlParkCommand = "SELECT [campground_id] ,[park_id],[name],[open_from_mm],[open_to_mm],[daily_fee] FROM [ParkReservation].[dbo].[campground] where park_id = @parkid";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@parkid", park.Id);
                cmd.CommandText = sqlParkCommand;
                cmd.Connection = connection;

                //Pull data off the table
                SqlDataReader reader = cmd.ExecuteReader();

                //Looping through the table and populating the list with all the rows
                while (reader.Read())
                {
                    Campground campground = new Campground();

                    campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    campground.ParkId = Convert.ToInt32(reader["park_id"]);
                    campground.Name = Convert.ToString(reader["name"]);
                    campground.OpenFrom = Convert.ToInt32(reader["open_from_mm"]);
                    campground.OpenTo = Convert.ToInt32(reader["open_to_mm"]);
                    campground.DailyFee = Convert.ToInt32(reader["daily_fee"]);


                    campgrounds.Add(campground);
                }
            }

            return campgrounds;
        }

        public List<CampSite> GetCampSitesInCampGround(Campground campground)
        {
            List<CampSite> campSites = new List<CampSite>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                const string sqlParkCommand = "SELECT [site_id],[campground_id],[site_number],[max_occupancy],[accessible],[max_rv_length],[utilities] FROM [ParkReservation].[dbo].[site] where campground_id = @campgroundID";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@campgroundId", campground.CampgroundId);
                cmd.CommandText = sqlParkCommand;
                cmd.Connection = connection;

                //Pull data off the table
                SqlDataReader reader = cmd.ExecuteReader();

                //Looping through the table and populating the list with all the rows
                while (reader.Read())
                {
                    CampSite campsite = new CampSite();

                    campsite.Id = Convert.ToInt32(reader["site_id"]);
                    campsite.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    campsite.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    campsite.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    campsite.Accesible = Convert.ToBoolean(reader["accessible"]);
                    campsite.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                    campsite.Utilities = Convert.ToBoolean(reader["utilities"]);
                    campSites.Add(campsite);
                }

            }

            return campSites;
        }

        public List<CampSite> GetAvailableReservations(Campground campground, DateTime arrivalDate, DateTime departureDate)
        {
            List<CampSite> listOfAvailableCampsites = new List<CampSite>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                const string sqlParkCommand = "select * from site where campground_id = @SiteId and site_id not in (Select site.site_id from site " +
                                                " Join reservation on reservation.site_id = site.site_id Where site.campground_id = @SiteId and " +
                                                "((From_date BETWEEN @arrivaldate AND @departuredate)  OR (To_date BETWEEN @arrivaldate AND @departuredate) " +
                                                "OR (From_date <= @arrivaldate AND To_date >= @departuredate) ))";


                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@arrivaldate", arrivalDate);
                cmd.Parameters.AddWithValue("@departuredate", departureDate);
                cmd.Parameters.AddWithValue("@SiteId", campground.CampgroundId);
                cmd.CommandText = sqlParkCommand;
                cmd.Connection = connection;

                //Pull data off the table
                SqlDataReader reader = cmd.ExecuteReader();

                //Looping through the table and populating the list with all the rows
                while (reader.Read())
                {
                    CampSite campSite = new CampSite();

                    campSite.Id = Convert.ToInt32(reader["site_id"]);
                    campSite.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    campSite.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    campSite.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    campSite.Accesible = Convert.ToBoolean(reader["accessible"]);
                    campSite.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                    campSite.Utilities = Convert.ToBoolean(reader["utilities"]);
                    listOfAvailableCampsites.Add(campSite);

                }


            }
            return listOfAvailableCampsites;
        }

        public List<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                const string sqlParkCommand = "SELECT [park_id],[name],[location],[establish_date],[area],[visitors],[description] FROM [ParkReservation].[dbo].[park]";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlParkCommand;
                cmd.Connection = connection;

                //Pull data off the table
                SqlDataReader reader = cmd.ExecuteReader();

                //Looping through the table and populating the list with all the rows
                while (reader.Read())
                {
                    Park park = new Park();

                    park.Id = Convert.ToInt32(reader["park_id"]);
                    park.Name = Convert.ToString(reader["name"]);
                    park.Location = Convert.ToString(reader["location"]);
                    park.EstablishDate = DateTime.Parse(reader["establish_date"].ToString());
                    park.Area = Convert.ToInt32(reader["area"]);
                    park.Visitors = Convert.ToInt32(reader["visitors"]);
                    park.Description = Convert.ToString(reader["description"]);

                    parks.Add(park);
                }
            }

            return parks;
        }

        //    //public HashSet<CampSite> ReturnAvailableCampsites(List<CampSite> campSites, List<Reservation> reservations)
        //    {
        //        List<CampSite> ReservedSites = new List<CampSite>(campSites);
        //        HashSet<CampSite> availableSites = new HashSet<CampSite>();
        //        foreach (var reservation in reservations)
        //        {
        //            foreach (var site in ReservedSites)
        //            {
        //                if(site.Id != reservation.SiteId)
        //                {

        //                }
        //            }

        //        }


        //        return availableSites;
        //    }
        //}
        public int WriteReservation(CampSite campsite, String reservationName, DateTime arrival, DateTime departure)
        {
            int reservationID;
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                connection.Open();

                const string sqlParkCommand = "Insert into reservation (site_id, name, from_date, to_date)" +
                                              " Values (@siteid, @name, @from_date, @to_date); SELECT CAST(SCOPE_IDENTITY() as int);";
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@siteid", campsite.Id);
                cmd.Parameters.AddWithValue("@name", reservationName);
                cmd.Parameters.AddWithValue("@from_date", arrival);
                cmd.Parameters.AddWithValue("@to_date", departure);
                cmd.CommandText = sqlParkCommand;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                reservationID = (int)cmd.ExecuteScalar();
            }
            return reservationID;
        }
    }
}
