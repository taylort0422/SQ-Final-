using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TMSMainWindow
{
    /// 
    /// \class Order
    ///
    /// \brief The purpose of this class is to keep all the parts of an order together
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    ///
    public class Order
    {
        public int Id { get; set; } ///</The order ID
        public int CustomerID { get; set; } ///</The customer associated with the order
        public string CustomerName { get; set; } ///</The name of the customer
        public int JobType { get; set; } ///</Type of job
        public int vanType { get; set; } ///</LTL or FTL
        public float TotalCost { get; set; } ///</The cost of this order
        public string OriginCity { get; set; } ///</Originating city
        public string DestinationCity { get; set; } ///</Final city
        public float TotalHours { get; set; } ///</The hours driven for the order
        public float TotalKms { get; set; } ///</The kms driven for the order
        public float Surcharge { get; set; } ///</Our markup on this order
        public float Markup { get; set; } ///</Markup % for this order
        public int OrderSize { get; set; }///</size in pallets (0 means FTL)
        public int Confirmed { get; set; } ///</Boolean indicating whether the order is confirmed
        public DateTime date { get; set; } ///<Date/time of the order

        public List<Trip> tripList = new List<Trip>(); ///<The name of the database 


        /// \brief To instantiate a new Order object 
        ///
        /// \return As this is a <i>constructor< / i> for the Communicate class, nothing is returned
        ///

        public Order(string customerName, string originCity, string destinationCity, int jobType, int vanType)
        {
            this.CustomerName = customerName;
            this.OriginCity = originCity;
            this.DestinationCity = destinationCity;
            this.JobType = jobType;
            this.vanType = vanType;
            this.Confirmed = 0;
        }

        ///
        /// \brief Changes an order's elements to a legible string
        /// \details <b>Details</b>
        /// 
        /// \param N/A
        /// 
        /// \return the elements of the order as a string
        public override string ToString()
        {
            return String.Format(Id + " " + CustomerName + " " + OriginCity + " " + DestinationCity + " " + JobType);
        }

        ///
        /// \brief Calculates the hours of an order
        /// \details <b>Details</b>
        /// Connects to a database, and finds a given order using an orderID. 
        /// The total hours of driving are calculated from all the trips within the order
        /// 
        /// \param (int) orderID
        /// 
        /// \return N/A
        public void CalcTotalHours(int orderID)
        {

            float totalHours = 0;
            float totalKms = 0;
            List<Trip> tripList = new List<Trip>();
            CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                            ConfigurationManager.AppSettings.Get("localPass"),
                            ConfigurationManager.AppSettings.Get("localIP"),
                            Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                            ConfigurationManager.AppSettings.Get("localDb"));

            tripList = nTMS.ListOfTrips(orderID);

            foreach (Trip trip in tripList)
            {
                totalHours += trip.Hours;
                totalHours += trip.Kilometers;
            }
            this.TotalHours = totalHours;
            this.TotalKms = totalKms;
        }
    }
}