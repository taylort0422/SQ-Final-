using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSMainWindow
{
    public class Contract
    {
        public int ContractID { get; set; }
        public string Name { get; set; }
        public int JobType { get; set; }
        public int Quantity { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int VanType { get; set; }

        public Contract(int ContractID,  string name, int jobType, int quantity, string origin, string destination, int vanType)
        {
            this.ContractID = ContractID;
            this.Name = name;
            this.JobType = jobType;
            this.Quantity = quantity;
            this.Origin = origin;
            this.Destination = destination;
            this.VanType = vanType;
        }
        public Contract()
        {

        }

        public override string ToString()
        {
            return String.Format(ContractID + " " + Name + " " + JobType + " " + Quantity + " " + Origin + " " + Destination + " " + VanType);
        }
    }
}