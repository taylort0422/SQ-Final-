using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TMSMainWindow
{

    public class CommTMS : Communicate
    {
        public string connStr { get; set; }
        public MySqlConnection conn = null;

        public CommTMS(string DbUserName, string DbPassword, string DbIP, int DbPort, string DbName) : base(DbUserName, DbPassword, DbIP, DbPort, DbName)
        {
            connStr = "server=" + DbIP + ";user=" + DbUser + ";database=" + DbName + ";port=" + DbPort + ";password=" + DbPassword;
            conn = new MySqlConnection(connStr);
        }

        public int InsertOrder(Order newOrder)
        {
            int retInt = 0;
            int customerID;
            //check if user exists first
            if ((customerID = CheckCustomer(newOrder.CustomerName)) != 0)
            {
                newOrder.CustomerID = customerID;
                retInt = 1;
                conn.Open();
                string sql = "INSERT INTO `order` (CustomerID, OrderType, VanType, DepartCity, DestCity, OrderConfirmed) VALUES(" + newOrder.CustomerID + "," + newOrder.JobType + "," + newOrder.vanType + ",\"" + newOrder.OriginCity + "\",\"" + newOrder.DestinationCity + "\"," + newOrder.Confirmed + "); SELECT LAST_INSERT_ID();";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    retInt = rdr.GetInt32(0);
                    //Console.WriteLine(rdr.GetString(0));
                }
                conn.Close();
            }
            else
            {
                retInt = 0;
            }


            return retInt;
        }

        public int CheckCustomer(string name)
        {
            conn.Open();
            string sql = "SELECT CustomerID FROM customer WHERE Name = \"" + name + "\"";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int retInt = 0;
            while (rdr.Read())
            {
                retInt = rdr.GetInt32(0);
            }
            conn.Close();
            return retInt;
        }

        public int AddCustomer(string name)
        {
            conn.Open();
            string sql = "INSERT INTO customer (name) VALUES(\"" + name + "\")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            conn.Close();

            return CheckCustomer(name);
        }

        public void AvailableCarriers(string cityL, int orderID)
        {
            List<string[]> cityList = new List<string[]>();

            //City list is a list of carriers available in each city that is passed in.
            string[] cities = cityL.Split(',');

            for (int i = 0; i < cities.Length; i++)
            {
                conn.Open();
                string sql = "SELECT CarrierID, Name FROM carrier WHERE DepotCity = \"" + cities[i].Trim() + "\"";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string[] array = { rdr.GetString(0), rdr.GetString(1) };
                    cityList.Add(array);
                }
                conn.Close();
            }

            for (int i = 0; i < cityList.Count; i++)
            {
                conn.Open();
                string sql = "INSERT INTO cityList (WhichOrder, DepotID, Carrier) VALUES(" + orderID + "," + cityList[i][0] + ",\"" + cityList[i][1].Trim() + "\")";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();
            }
        }

        public List<Order> ListOpenOrders()
        {
            List<Order> oList = new List<Order>();
            conn.Open();
            string sql = "SELECT CustomerID, DepartCity, DestCity, OrderType, VanType, OrderID FROM `order` WHERE OrderConfirmed = 0";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Order no = new Order(null, rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4));
                no.CustomerID = rdr.GetInt32(0);
                no.Id = rdr.GetInt32(5);
                oList.Add(no);
            }
            conn.Close();
            for (int c = 0; c < oList.Count; c++)
            {
                oList[c].CustomerName = GetCustomerName(oList[c].CustomerID);
            }

            return oList;
        }

        public string GetCustomerName(int custID)
        {
            string retString = null;
            conn.Open();
            string sql = "SELECT name FROM customer WHERE CustomerID = " + custID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                retString = rdr.GetString(0);
            }
            conn.Close();
            return retString;
        }

        public int GetOrderID(string Name)
        {
            int retString = 0;
            conn.Open();
            string sql = "SELECT OrderID FROM customer WHERE name = " + Name.Trim();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                retString = rdr.GetInt32(0);
            }
            conn.Close();
            return retString;
        }

        public List<string> DisplayCarriers(int orderID)
        {

            List<string> cities = new List<string>();
            conn.Open();
            string sql = "SELECT DepotID, Carrier, carrier.DepotCity " 
                        + "FROM citylist "
                        + "INNER JOIN carrier ON citylist.DepotID = carrier.CarrierID WHERE WhichOrder = " + orderID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string retString = rdr.GetString(0) + " : " + rdr.GetString(1) + " : " + rdr.GetString(2);
                cities.Add(retString);
            }
            conn.Close();
            return cities;
        }

        public string GetRouteKMSHRS(string depart, string destination, string direction)
        {
            int firstRouteID = 0;
            int lastRouteID = 0;


            float totalKMs = 0;
            float totalHours = 0;


            conn.Open();
            string sql = "SELECT routeID FROM route WHERE DepartLocation = \"" + depart + "\" AND Direction = \"" + direction + "\"";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                firstRouteID = rdr.GetInt32(0);
            }
            conn.Close();

            conn.Open();
            sql = "SELECT routeID FROM route WHERE DestinationLocation = \"" + destination + "\" AND Direction = \"" + direction + "\";";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                lastRouteID = rdr.GetInt32(0);
            }
            conn.Close();



            for (int i = firstRouteID; i <= lastRouteID; i++)
            {
                conn.Open();
                sql = "SELECT KMS, Hours FROM route WHERE RouteID = " + i;

                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    totalKMs += rdr.GetFloat(0);
                    totalHours += rdr.GetFloat(1);
                }
                conn.Close();
                //iterate through all routes in the table that could make up this trip

            }

            return (totalKMs + "|" + totalHours);
        }

        public void PlanTrip(string[] trip, int orderNum)
        {

            foreach (string t in trip) 
            {
                float hours = 0;
                float kms = 0;
                string[] kmhrs = null;
                string[] trip1 = t.Split('-');//seperate each trip into route and destination

                string DepLocation = GetCityFromCarrier(Int32.Parse(trip1[0]));
                string DestLocation = GetCityFromCarrier(Int32.Parse(trip1[1]));

                // should be checked against greater than 8
                if (Int32.Parse(trip1[0]) < Int32.Parse(trip1[1]))
                {
                    //Heading east
                    kmhrs = GetRouteKMSHRS(DepLocation, DestLocation, "east").Split('|');
                    kms = float.Parse(kmhrs[0]);
                    hours = float.Parse(kmhrs[1]);

                }
                else if (Int32.Parse(trip1[0]) > Int32.Parse(trip1[1]))
                {
                    //Heading west
                    kmhrs = GetRouteKMSHRS(DepLocation, DestLocation, "west").Split('|');
                    kms = float.Parse(kmhrs[0]);
                    hours = float.Parse(kmhrs[1]);

                }

                Trip newTrip = new Trip();
                newTrip.OrderId = orderNum;
                newTrip.OriginCity = DepLocation;
                newTrip.DestinationCity = DestLocation;
                newTrip.Kilometers = kms;
                newTrip.Hours = hours;

                InsertTrip(newTrip);
            }

        }

        public string GetCityFromCarrier(int CarrierID)
        {
            string city = "";
            conn.Open();
            string sql = "SELECT DepotCity FROM carrier WHERE CarrierID= " + CarrierID;

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                city = rdr.GetString(0);
            }
            conn.Close();
            return city;

        }

        public int InsertTrip(Trip newTrip)
        {
            int lastID = 0;

            conn.Open();
            string sql = "INSERT INTO `trip` (OrderID, OriginCity, DestinationCity, Kilometers, Hours) VALUES(" + newTrip.OrderId + ",\"" + newTrip.OriginCity + "\",\"" + newTrip.DestinationCity + "\"," + newTrip.Kilometers + "," + newTrip.Hours + "); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lastID = rdr.GetInt32(0);
                //Console.WriteLine(rdr.GetString(0));
            }
            conn.Close();
            return lastID;
        }

        public List<Trip> ListOfTrips(int orderId)
        {
            List<Trip> tripList = new List<Trip>();
            Trip trip = new Trip();
            conn.Open();
            string sql = "SELECT TripID, OriginCity, DestinationCity, Kilometers, Hours FROM trip WHERE OrderID = " + orderId;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                trip.ID = rdr.GetInt32(0);
                trip.OrderId = orderId;
                trip.OriginCity = rdr.GetString(1);
                trip.DestinationCity = rdr.GetString(2);
                trip.Kilometers = rdr.GetFloat(3);
                trip.Hours = rdr.GetFloat(4);
                tripList.Add(trip);
            }
            conn.Close();

            return tripList;
        }

        public List<string> CitiesBetween(string depart, string destination, string direction)
        {
            int firstRouteID=0;
            int lastRouteID=0;
            //string direction = null;
            //Need to determine direction
            //if (firstRouteID < 8) direction = "East";
           // else direction = "West";


            conn.Open();
            string sql = "SELECT routeID FROM route WHERE DepartLocation = \"" + depart + "\" AND Direction = \"" + direction + "\"";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                firstRouteID = rdr.GetInt32(0);
            }
            conn.Close();

         

            conn.Open();
            sql = "SELECT routeID FROM route WHERE DestinationLocation = \"" + destination + "\" AND Direction = \"" + direction + "\"";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                lastRouteID = rdr.GetInt32(0);
            }
            conn.Close();




            List<string> cities = new List<string>();
            for (int i = firstRouteID; i <= lastRouteID; i++)
            {
                conn.Open();
                sql = "SELECT DepartLocation FROM route WHERE RouteID = " + i;

                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cities.Add(rdr.GetString(0));
                }
                conn.Close();
            }
            cities.Add(destination);
            return cities;
        }

        public List<string> DisplayCustomers()
        {

            List<string> customers = new List<string>();
            conn.Open();
            string sql = "SELECT Name FROM customer";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string retString = rdr.GetString(0);
                customers.Add(retString);
            }
            conn.Close();
            return customers;
        }
    }
}