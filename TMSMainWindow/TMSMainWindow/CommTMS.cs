using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TMSMainWindow
{
    /// 
    /// \class Comm TMS
    ///
    /// \brief This class is a child of the Communicate class, and has more specialized methods
    /// 
    /// \details <b>Details< / b>
    /// The Communicate has the same parameters as it's parent class. This class is far more involved with  
    /// databases however, and can see, retreive, insert, and update all sorts of fields within our TMS database. 
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    /// 
    /// \see Communicate.cs
    ///

    public class CommTMS : Communicate
    {
        public string connStr { get; set; } ///<SQL command to connect to databse
        public MySqlConnection conn = null;

/*
* \brief To instantiate a new CommTMS object 
*
* \return As this is a <i>constructor< / i> for the CommTMS class, nothing is returned
*/
        public CommTMS(string DbUserName, string DbPassword, string DbIP, int DbPort, string DbName) : base(DbUserName, DbPassword, DbIP, DbPort, DbName)
        {
            connStr = "server=" + DbIP + ";user=" + DbUser + ";database=" + DbName + ";port=" + DbPort + ";password=" + DbPassword;
            conn = new MySqlConnection(connStr);
        }

        ///
        /// \brief Called to insert a new order into the database
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and inserts a newly created order into    
        /// an SQL database
        /// 
        /// \param Order object 
        /// \see Order.cs
        /// 
        /// \return an int indicating success
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
                }
                conn.Close();
            }
            else
            {
                retInt = 0;
            }


            return retInt;
        }

        ///
        /// \brief Called to see if a customer exists
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and searches for a specific customer
        /// 
        /// \param (string) customer name
        /// 
        /// \return an int of a customer's ID if method is successful
        /// 
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

        ///
        /// \brief Called to add a customer to the database
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and inserts a customer
        /// 
        /// \param (string) customer name
        /// 
        /// \return an int of a customer's ID if method is successful
        /// 
        public int AddCustomer(string name)
        {
            conn.Open();
            string sql = "INSERT INTO customer (name) VALUES(\"" + name + "\")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            conn.Close();

            return CheckCustomer(name);
        }

     
        ///
        /// \brief view carriers in a city
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, checks cities against an orderID. The method will see which
        /// cities are within a specific order. The method then connects to a different database table, and updates it with
        /// the cities within a specific order
        /// 
        /// \param (string) cityL, (int) orderID
        /// 
        /// \return N/A
        /// 
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

        ///
        /// \brief Called to generate a list of open orders
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, goes through the 'orders' table. All of the orders with a 0 status for 'confirmed' 
        /// (so not confirmed) will be pulled from the database. Every order which has not yet been confirmed will 
        /// be added to a list.
        /// 
        /// \param N/A
        /// 
        /// \return list of open orders
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

        ///
        /// \brief Called to generate a list of confirmed orders
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, goes through the 'orders' table. All of the orders with a 1 status for 'confirmed' 
        /// (so  confirmed) will be pulled from the database. Every order which has  been confirmed will 
        /// be added to a list.
        /// 
        /// \param N/A
        /// 
        /// \return list of completed orders
        public List<Order> ListCompletedOrders()
        {
            List<Order> oList = new List<Order>();
            conn.Open();
            // Look for all orders that are completed
            string sql = "SELECT CustomerID, DepartCity, DestCity, OrderType, VanType, OrderID FROM `order` WHERE OrderConfirmed = 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                // Build a temporary order of each order
                Order no = new Order(null, rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4));
                no.CustomerID = rdr.GetInt32(0);
                no.Id = rdr.GetInt32(5);
                // Add to list
                oList.Add(no);
            }
            conn.Close();
            for (int c = 0; c < oList.Count; c++)
            {
                oList[c].CustomerName = GetCustomerName(oList[c].CustomerID);
            }
            // Return list of open orders
            return oList;
        }

        ///
        /// \brief Called to retreive a customer name from their custID
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and finds a specific customer ID. The name associated
        /// with that ID is retreived.
        /// 
        /// \param (int) custID
        /// 
        /// \return (string) customer's name
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
            // Return the customer name
            return retString;
        }

        ///
        /// \brief Called to retreive a list of carriers and their cities based on an OrderID
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and finds a specific order ID from a cityList table. 
        /// all associated cities from that orderID are returned. 
        /// 
        /// \param (int) orderID
        /// 
        /// \return (san array of (string) showing the cities within that order 
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

        // This calculates the KMs and Hrs a route takes

        ///
        /// \brief Calculates the KM's and hours a route takes
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and finds a specific route ID from the routes table. For 
        /// each city the KM's and hours will be taken out of that route. These values will be added together to determine 
        /// the length of the trip. 
        /// 
        /// \param (string) depart location, (string) destination location, (string) direction
        /// 
        /// \return a string indicating the KM's and horus of a route


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


            // Iterate through all routes between the origin city of this trip and the destination city and calculate total KMS and hours
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
            // Return a formatted string 
            return (totalKMs + "|" + totalHours);
        }

        // Plan trip builds a trip to later be inserted 

        ///
        /// \brief Called to insert a planned trip into the database
        /// \details <b>Details</b>
        /// 
        /// Method gets destionation and departure cities from another fucntion. 
        /// After the cities and direction are determined, the trip is inserted into a database.
        /// 
        /// \param (string) trip, (int) order number
        /// 
        /// \return N/A
        public void PlanTrip(string trip, int orderNum)
        {

            float hours = 0;
            float kms = 0;
            string[] kmhrs = null;
            string[] trip1 = trip.Split('-');//seperate each trip into route and destination

            // Get the city names from the carrier IDs
            string DepLocation = GetCityFromCarrier(Int32.Parse(trip1[0]));
            string DestLocation = GetCityFromCarrier(Int32.Parse(trip1[1]));

            // should be checked against greater than 8
            //if (Int32.Parse(trip1[0]) < Int32.Parse(trip1[1]))
            if (Int32.Parse(trip1[0]) < 8)
            {
                //Heading east
                kmhrs = GetRouteKMSHRS(DepLocation, DestLocation, "east").Split('|');
                kms = float.Parse(kmhrs[0]);
                hours = float.Parse(kmhrs[1]);

            }
            //else if (Int32.Parse(trip1[0]) > Int32.Parse(trip1[1]))
            else if (Int32.Parse(trip1[0]) >= 8)
            {
                //Heading west
                kmhrs = GetRouteKMSHRS(DepLocation, DestLocation, "west").Split('|');
                kms = float.Parse(kmhrs[0]);
                hours = float.Parse(kmhrs[1]);

            }

            Trip newTrip = new Trip();
            newTrip.OriginDepotID = Int32.Parse(trip1[0]);
            newTrip.DestinationDepotID = Int32.Parse(trip1[1]);
            newTrip.OrderId = orderNum;
            newTrip.OriginCity = DepLocation;
            newTrip.DestinationCity = DestLocation;
            newTrip.Kilometers = kms;
            newTrip.Hours = hours;

            InsertTrip(newTrip);
        }

        

        ///
        /// \brief Called to updates the order to completed
        /// \details <b>Details</b>
        /// 
        /// Method chamnges an OrderConfirmed value to '1'
        /// 
        /// \param (int) orderID
        /// 
        /// \return N/A
        public void MarkOrderComplete(int orderID)
        {
            conn.Open();
            string sql = "UPDATE `order` SET OrderConfirmed = 1, OrderDate = \"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\" WHERE OrderID = " + orderID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

        }


        ///
        /// \brief Called to find a carrier with a specific name in a specific city
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for a carrier name and a city. The carrierID value
        /// will be retreived from that entry in the database where the name and city match those of the database
        /// 
        /// \param (string) name, (string) city
        /// 
        /// \return (int) carrierID
        public int GetCarrierID(string name, string city)
        {
            int cityID = 0;
            conn.Open();
            string sql = "SELECT CarrierID FROM carrier WHERE Name = \"" + name + "\" AND DepotCity = \"" + city + "\";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                cityID = rdr.GetInt32(0);
            }
            conn.Close();

            return cityID;
        }


        ///
        /// \brief Called to get the Depot city of a carrier based on it's ID
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for a carrierID. The depot city of the 
        /// associated carrierID will be returned
        /// 
        /// \param (int) carrierID
        /// 
        /// \return (string) city
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

        ///
        /// \brief Called to insert a trip into the database
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for a carrierID. The depot city of the 
        /// associated carrierID will be returned
        /// 
        /// \param (int) carrierID
        /// 
        /// \return (string) city
        public int InsertTrip(Trip newTrip)
        {
            int lastID = 0;

            conn.Open();
            string sql = "INSERT INTO `trip` (OrderID, OriginCity, DestinationCity, Kilometers, Hours, OriginDepotID, DestinationDepotID) VALUES(" + newTrip.OrderId + ",\"" + newTrip.OriginCity + "\",\"" + newTrip.DestinationCity + "\"," + newTrip.Kilometers + "," + newTrip.Hours + "," + newTrip.OriginDepotID + "," + newTrip.DestinationDepotID + "); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lastID = rdr.GetInt32(0);
            }
            conn.Close();
            return lastID;
        }

        /// \brief Called to create a list of trips from a specific order
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for an orderID. According to that orderID,
        /// the database is searched for all trips associated wit that order. 
        /// Those trips are added to a list, and returned.
        /// 
        /// \param (int) orderID
        /// 
        /// \return List of trips
        public List<Trip> ListOfTrips(int orderId)
        {
            List<Trip> tripList = new List<Trip>();
            //Trip trip = new Trip();
            conn.Open();
            string sql = "SELECT TripID, OriginCity, DestinationCity, Kilometers, Hours, OriginDepotID, DestinationDepotID FROM trip WHERE OrderID = " + orderId;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                // Build the temporary trip from DB
                Trip trip = new Trip();
                trip.ID = rdr.GetInt32(0);
                trip.OrderId = orderId;
                trip.OriginCity = rdr.GetString(1);
                trip.DestinationCity = rdr.GetString(2);
                trip.Kilometers = rdr.GetFloat(3);
                trip.Hours = rdr.GetFloat(4);
                trip.OriginDepotID = rdr.GetInt32(5);
                trip.DestinationDepotID = rdr.GetInt32(6);
                // Add to the lost of trips
                tripList.Add(trip);
            }
            conn.Close();
            // Return the list of trips
            return tripList;
        }

        /// \brief Called to find a specific carrier from their ID
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for a carrierID. According to that ID,
        /// the database is searched for the associated carrrier. 
        /// 
        /// \param (int) carrierID
        /// 
        /// \return (string) name of carrier
        public string GetCarrierFromID(int carrierID)
        {
            string retString = "";
            conn.Open();
            string sql = "SELECT Name, DepotCity FROM carrier WHERE CarrierID = " + carrierID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                retString = rdr.GetString(0) + " - " + rdr.GetString(1);
            }
            conn.Close();
            return retString;
        }


        /// \brief Called to find all cities between an origin and a destination
        /// \details <b>Details</b>
        /// 
        /// Method searches between a departure and destination city, and find each city between them
        /// 
        /// \param (string) depart, (string) destination, (string) direction
        /// 
        /// \return List of strings of cities between the depart and destination
        public List<string> CitiesBetween(string depart, string destination, string direction)
        {
            int firstRouteID=0;
            int lastRouteID=0;
            //string direction = null;
            //Need to determine direction
            //if (firstRouteID < 8) direction = "East";
           // else direction = "West";

            // Get the route ID of the Origin City
            conn.Open();
            string sql = "SELECT routeID FROM route WHERE DepartLocation = \"" + depart + "\" AND Direction = \"" + direction + "\"";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                firstRouteID = rdr.GetInt32(0);
            }
            conn.Close();

            // Get the route ID of the destination city
            conn.Open();
            sql = "SELECT routeID FROM route WHERE DestinationLocation = \"" + destination + "\" AND Direction = \"" + direction + "\"";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {

                lastRouteID = rdr.GetInt32(0);
            }
            conn.Close();

            // Begin creating a list of all routes between origin and destination
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
            // Return the list of cities
            return cities;
        }

        /// \brief Displays customers
        /// \details <b>Details</b>
        /// 
        /// Method searches the contract database and finds all active customers
        /// 
        /// \param N/A
        /// 
        /// \return List of strings of customers
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


        /// \brief Called to get a list of carriers from the database and their data
        /// \details <b>Details</b>
        /// 
        /// Method searches a database, extracts all carrier information, and returns it in a list 
        /// of objects
        /// 
        /// \param N/A
        /// 
        /// \return List of Carrier objects
        public List<Carrier> CarrierList()
        {
            List<Carrier> carriers = new List<Carrier>();
            string sql = "SELECT * FROM carrier;";
            conn.Open();
            //Open the database
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
                // Get the carrier data and build a temp carrier
                Carrier c = new Carrier();
                c.CarrierID = rdr.GetInt32(0);
                c.CarrierName = rdr.GetString(1);
                c.DepotCity = rdr.GetString(2);
                c.FTLA = rdr.GetInt32(3);
                c.LTLA = rdr.GetInt32(4);
                c.FTLRate = rdr.GetFloat(5);
                c.LTLRate = rdr.GetFloat(6);
                c.ReefCharge = rdr.GetFloat(7);
                // Add to the list of carriers
                carriers.Add(c);
            }
            conn.Close();
            // Return the list of all carriers
            return carriers;
        }


        /// \brief Called to generate an invoice
        /// \details <b>Details</b>
        /// 
        /// Method searches a database by orderID, geenrates an invoice based off of
        /// order information and costs
        /// 
        /// \param (int) orderID
        /// 
        /// \return invoice


        ///RemoveTrip(int TripID)
        /// \brief Called to delete a trip
        /// \details <b>Details</b>
        /// 
        /// Method searches a database based on tripID, and removes that trip from the database
        /// 
        /// \param (int) tripID
        /// 
        /// \return N/A

        /// \brief Called to insert an invoice on to an order
        /// \details <b>Details</b>
        /// 
        /// Method searches a database based on OrderID, and inserts the associated invoice
        /// 
        /// \param (int) orderID, (int) invoiceID
        /// 
        /// \return N/A

        /// \brief Called to back up a database
        /// \details <b>Details</b>
        /// 
        /// Method backs up a database to a specified file path
        /// 
        /// \param (string)filePath
        /// 
        /// \return N/A

        ///UpdateCarrier(int CarrierID)
        /// \brief Called to update a carrier's information
        /// \details <b>Details</b>
        /// 
        /// Method searches a database for a carrier based on their ID, and gives
        /// user ability to update any of the carrier's attributes
        /// 
        /// \param (int)carrierID
        /// 
        /// \return N/A
    }
}