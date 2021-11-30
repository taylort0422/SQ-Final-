using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TMSMainWindow
{
    public class Trip
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int TruckId { get; set; }
        public float TripCost { get; set; }
        public float GrossCost { get; set; }
        public float Kilometers { get; set; }
        public float Hours { get; set; }
        public int OriginDepotID { get; set; }
        public string OriginCity { get; set; }
        public int DestinationDepotID { get; set; }    
        public string DestinationCity { get; set; }

        private CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                        ConfigurationManager.AppSettings.Get("localPass"),
                        ConfigurationManager.AppSettings.Get("localIP"),
                        Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                        ConfigurationManager.AppSettings.Get("localDb"));

        public float TripTotalCost(Trip trip)
        {
            float totalCost = 0;

            List <Carrier> carrierList = new List<Carrier>();
            List <Order> orderList = new List<Order>();

            orderList = nTMS.ListCompletedOrders();
            carrierList = nTMS.CarrierList();

            var tripCarrier = carrierList.FirstOrDefault(o => o.CarrierID == trip.OriginDepotID);
            var tripOrder = orderList.FirstOrDefault(t => t.Id == trip.OrderId);

            if(tripOrder.JobType == 0)
            {
                totalCost = Kilometers * tripCarrier.FTLRate;
            }
            else if(tripOrder.JobType == 1)
            {
                totalCost = Kilometers * tripCarrier.LTLRate;
            }

            if(tripOrder.vanType == 1)
            {
                totalCost += totalCost * tripCarrier.ReefCharge;
            }
            

            return totalCost;
        }
    }
}