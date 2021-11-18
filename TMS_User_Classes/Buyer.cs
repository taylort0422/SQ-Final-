using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_Users
{
    class Buyer : User
    {

        //Connect to Contract Marketplace and review customer
        void ReviewCustomer()
        { 
            string connectString = @"Data Source=" + "localhost" + ";Initial Catalogue=" + "TMS" + ";Persist Security Info=True;User ID=user";
        }
        //Connect to Contract Marketplace and add customer
        void AcceptCustomer(string customer)
        { }
        //Connect to Contract Marketplace, pick a contract and create an order with it
        void CreateOrder()
        { }
        //Select route cities to complete the order
        void AddCitiesToOrder()
        { }
        //Generate a final invoice of the items of a final bill
        void GenerateInvoice(string order)
        {
        }
    }
}
