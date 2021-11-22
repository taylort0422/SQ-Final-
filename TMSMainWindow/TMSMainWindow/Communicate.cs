using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace TMSMainWindow
{
    public class Communicate
    {
        public string DbIP { get; set; }
        public int DbPort { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string DbName { get; set; }

        public Communicate(string DbUserName, string DbPassword, string DbIP, int DbPort, string DbName)
        {
            this.DbIP = DbIP;
            this.DbPort = DbPort;
            this.DbUser = DbUserName;
            this.DbPassword = DbPassword;
            this.DbName = DbName;
        }

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


                while (rdr.Read())
                {
                    Contract newContract = new Contract();
                    newContract.Name = rdr[0].ToString();
                    newContract.JobType = Int32.Parse(rdr[1].ToString());
                    newContract.Quantity = Int32.Parse(rdr[2].ToString());
                    newContract.Origin = rdr[3].ToString();
                    newContract.Destination = rdr[4].ToString();
                    newContract.VanType = rdr.GetInt32(5);
                    ConList.Add(newContract);
                }
                rdr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.ReadKey();
            conn.Close();
            return ConList;
        }
    }
}