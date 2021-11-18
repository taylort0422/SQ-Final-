using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Users
{
    class Planner : User
    {
        //Get initial order from the buyer to create a route using carriers
        void ReceiveOrder()
        { }

        //Simulate time passing to get updates on carrier status
        void SimulateTimePassing(int Days)
        { }

        //Complete order after completing route
        void MarkOrderAsComplete()
        { }

        //Display all active orders in a status screen
        void GetSummaryOfAllOrders()
        { }

        //Generate a summary report of all invoice data for either all time or the past two weeks.
        void GenerateReport(int timePeriod)
        { }
    }
}
