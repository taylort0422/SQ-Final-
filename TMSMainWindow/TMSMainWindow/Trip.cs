using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}