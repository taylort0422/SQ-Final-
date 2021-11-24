using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSMainWindow
{
    /// 
    /// \class Contract
    ///
    /// \brief The purpose of this class is to keep all the parts of a contract together
    /// 
    /// \author <i>Ashley Ingle + Andrew Tudor + Will Schwetz + Taylor Trainor</i>
    ///
    public class Contract
    {
        public int ContractID { get; set; } ///</The contract ID
        public string Name { get; set; } ///</The customer name 
        public int JobType { get; set; } ///</The type of job
        public int Quantity { get; set; } ///</Amount of boxes on the order
        public string Origin { get; set; } ///</Origin city
        public string Destination { get; set; } ///</The final city
        public int VanType { get; set; } ///</LTL or FTL

    /*
    * \brief To instantiate a new Contract object 
    *
    * \return As this is a <i>constructor< / i> for the Contract class, nothing is returned
    */
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

        ///
        /// \brief Changes an contract's elements to a legible string
        /// 
        /// \param N/A
        /// 
        /// \return the elements of the contract as a string
        public override string ToString()
        {
            return String.Format(ContractID + " " + Name + " " + JobType + " " + Quantity + " " + Origin + " " + Destination + " " + VanType);
        }
    }
}