using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Users
{
    class Admin : User
    {
        //Create a new log file at chosen directory
        void SetLogFileDirectory()
        { }

        //Select database name, username, password, IP address and port of database to connect to
        void ConfigureDMBSComms()
        { }

        //Read previously created log files
        void ReviewLogfiles()
        { }

        //Create a new database that is a backup of the existing local tms database
        void CreateLocalDatabaseBackup()
        { }
      
        //Modify the table of carrier data as well as rates/fees that are associated with carriers
        void AlterCarrierData()
        { }

        // Modify the route table
        void AlterRouteTable()
        { }
    }
}
