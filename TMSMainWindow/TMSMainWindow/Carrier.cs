using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSMainWindow
{
    /// 
    /// \class Carrier
    ///
    /// \brief This class represents a carrier in the system
    /// 
    /// \details <b>Details< / b>
    /// The carrier class contains all the information about a carrier
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    /// 
    ///
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
