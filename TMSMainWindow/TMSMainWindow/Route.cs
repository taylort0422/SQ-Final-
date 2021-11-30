using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSMainWindow
{
    public class Route
    {
        public int RouteID { get; set; }
        public string DepartLocation { get; set; }
        public string DestinationLocation { get; set; }
        public float Hours { get; set; }
        public int KMs { get; set; }
        public string Direction { get; set; }
    }
}
