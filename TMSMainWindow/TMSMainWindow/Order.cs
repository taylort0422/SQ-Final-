using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TMSMainWindow
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int JobType { get; set; }
        public int vanType { get; set; }
        public float TotalCost { get; set; }
        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public float TotalHours { get; set; }
        public float TotalKms { get; set; }
        public float Surcharge { get; set; }
        public float Markup { get; set; }
        public int Confirmed { get; set; }
        public DateTime date { get; set; }

        public List<Trip> tripList = new List<Trip>();

        public Order(string customerName, string originCity, string destinationCity, int jobType, int vanType)
        {
            this.CustomerName = customerName;
            this.OriginCity = originCity;
            this.DestinationCity = destinationCity;
            this.JobType = jobType;
            this.vanType = vanType;
            this.Confirmed = 0;
        }

        public override string ToString()
        {
            return String.Format(Id + " " + CustomerName + " " + OriginCity + " " + DestinationCity + " " + JobType);
        }

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