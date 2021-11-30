using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TMSMainWindow;
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

            Console.Write(nTMS.GenerateInvoice(44));
            Console.ReadKey();
        }
    }
}
