using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TMSMainWindow
{
    /// 
    /// \class Communicate
    ///
    /// \brief The purpose of this class is to allow communications between
    /// our program and a database
    /// 
    /// \details <b>Details< / b>
    /// The Communicate has several parameters; (string) DbUserName to access the database,
    /// (string) DbPassword to also access a database, a (string) DbIP to conncet
    /// to the database IP, an (int) DbPort to also connect to the database IP, and
    /// a (string) DbName as a database's name.
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    ///
    public class Communicate
    {
        public string DbIP { get; set; }  ///<The IP of the database 
        public int DbPort { get; set; } ///<The Port of the database 
        public string DbUser { get; set; }  ///<The username for the database 
        public string DbPassword { get; set; }  ///<The password of the database 
        public string DbName { get; set; }  ///<The name of the database 

/*
* \brief To instantiate a new Communicate object 
*
* \return As this is a <i>constructor< / i> for the Communicate class, nothing is returned
*/
        public Communicate(string DbUserName, string DbPassword, string DbIP, int DbPort, string DbName)
        {
            this.DbIP = DbIP;
            this.DbPort = DbPort;
            this.DbUser = DbUserName;
            this.DbPassword = DbPassword;
            this.DbName = DbName;
        }

        ///
        /// \brief Called to retreive a list of contracts from the database
        /// \details <b>Details</b>
        /// 
        /// Method connects to a server, and goes through all available contracts. 
        /// Each contracted will be added to a list, which will be returned by th emethod
        /// 
        /// \param N/A
        /// 
        /// \return the list of contracts
        public List<Contract> RetrieveContracts()
        {
            string connStr = "server=" + DbIP + ";user=" + DbUser + ";database=" + DbName + ";port=" + DbPort + ";password=" + DbPassword;
            MySqlConnection conn = new MySqlConnection(connStr);
            List<Contract> ConList = new List<Contract>();

            try
            {
                conn.Open();
                string sql = "SELECT * FROM Contract";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                int i = 0;
                while (rdr.Read())
                {
                    Contract newContract = new Contract();
                    newContract.ContractID = i;
                    newContract.Name = rdr[0].ToString();
                    newContract.JobType = Int32.Parse(rdr[1].ToString());
                    newContract.Quantity = Int32.Parse(rdr[2].ToString());
                    newContract.Origin = rdr[3].ToString();
                    newContract.Destination = rdr[4].ToString();
                    newContract.VanType = rdr.GetInt32(5);
                    ConList.Add(newContract);
                    i++;
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            conn.Close();
            return ConList;
        }
    }
}