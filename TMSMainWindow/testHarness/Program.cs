using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TMSMainWindow;
using Newtonsoft.Json;
namespace testHarness
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                                ConfigurationManager.AppSettings.Get("localPass"),
                                ConfigurationManager.AppSettings.Get("localIP"),
                                Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                                ConfigurationManager.AppSettings.Get("localDb"));

            //List<string> results = JsonConvert.DeserializeObject<List<string>>(nTMS.GetRouteTable());

            Console.Write(nTMS.forwardTrip(43, DateTime.Now.AddHours(24)));
            Console.ReadKey();
        }
    }
}
