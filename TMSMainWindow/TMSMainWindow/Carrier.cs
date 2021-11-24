using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSMainWindow
{
    public class Carrier
    {
        public int CarrierID { get; set; }
        public string CarrierName { get; set; }
        public string DepotCity { get; set; }
        public int FTLA { get; set; }
        public int LTLA { get; set;}
        public float FTLRate { get; set; }
        public float LTLRate { get; set; }
        public float ReefCharge { get; set; }
    }
}
