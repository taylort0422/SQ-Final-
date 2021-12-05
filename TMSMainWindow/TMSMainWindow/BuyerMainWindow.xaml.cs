using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace TMSMainWindow
{
    /// <summary>
    /// Interaction logic for BuyerMainWindow.xaml
    /// </summary>
    public partial class BuyerMainWindow : Window
    {
        /// A private variable.
        ///
        /// This variable stores the communication information for the local database.
        ///
        private CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                                ConfigurationManager.AppSettings.Get("localPass"),
                                ConfigurationManager.AppSettings.Get("localIP"),
                                Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                                ConfigurationManager.AppSettings.Get("localDb"));
        /// A private variable.
        ///
        /// This variable stores the communication information for the external marketplace database.
        ///
        private Communicate nc = new Communicate(ConfigurationManager.AppSettings.Get("marketplaceUser"),
                                    ConfigurationManager.AppSettings.Get("marketplacePass"),
                                    ConfigurationManager.AppSettings.Get("marketplaceIP"),
                                    Int32.Parse(ConfigurationManager.AppSettings.Get("marketplacePort")),
                                    ConfigurationManager.AppSettings.Get("marketplaceDb"));
        /// A private variable.
        ///
        /// This variable stores the information for the Order currently being created.
        /// 
        private Order newOrder;
        /// A function variable.
        ///
        /// This string is a list of cities that the Order will stop at.
        /// \sa newOrder
        ///
        string sCities = "";
        /// A function variable.
        ///
        /// This is a list that will be filled with contracts from the Contract database.
        ///
        List<Contract> contracts = new List<Contract>();

        /// \brief This is the function for the Buyer window. It fills the relevant WPF controls with data from the external and internal databases.
        /// \param N/A
        /// \return N/A
          
        public BuyerMainWindow()
        {
            InitializeComponent();
            contracts = nc.RetrieveContracts();
            foreach (Contract contract in contracts)
            {
                MarketplaceOrdersListBox.Items.Add(contract);
            }

            foreach(string customer in nTMS.DisplayCustomers())
            {
                AcceptedCustomerListBox.Items.Add(customer);
            }

            foreach(Order o in nTMS.ListCompletedOrders())
            {
                CompletedOrdersListBox.Items.Add(o);
            }
        }

        /// \brief This is the function that is triggered when an item is selected from the Marketplace Orders menu.
        ///
        /// \details It gets the selected order from the Marketplace Orders list and starts creating a new order.
        /// \param sender the wpf object that called the function.
        /// \param e the arguments of the event that was triggered.
        /// \return void
        
        private void MarketplaceOrdersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int palletSize = 0;
            int selectedContract;
     
            // Get the ID of the selected Order
            string choice = MarketplaceOrdersListBox.SelectedItem.ToString().Split(',')[0].Trim();
            palletSize = Int32.Parse(MarketplaceOrdersListBox.SelectedItem.ToString().Split(',')[3].Trim());
            selectedContract = Int32.Parse(MarketplaceOrdersListBox.SelectedItem.ToString().Split(',')[0].Trim());
            AddCityToOrderButton.IsEnabled = true;

            //Begin creating the new order

            newOrder = new Order(contracts[selectedContract].Name, contracts[selectedContract].Origin, contracts[selectedContract].Destination, contracts[selectedContract].JobType, contracts[selectedContract].VanType);
            newOrder.OrderSize = palletSize;
            if (nTMS.CheckCustomer(newOrder.CustomerName) == 0)
            {
                NewCustomerListBox.Items.Add(newOrder.CustomerName);
                NewCustomerListBox.IsEnabled = true;
                AcceptSelectedCustomerButton.IsEnabled = true;
            }
            else
            {
                CreateNewOrderButton.IsEnabled=true;
            }

            MarketplaceOrdersListBox.IsEnabled = false;
            // 0 = FTL 1 = LTL
            if(newOrder.JobType == 0)
            {
                SelectCityToAddDropdown.IsEnabled = false;
                AddCityToOrderButton.IsEnabled = false;
                //CreateNewOrderButton.IsEnabled = true;
            }
            else
            {
                //populate cities to add
                foreach (string city in nTMS.CitiesBetween(newOrder.OriginCity, newOrder.DestinationCity, "East"))
                {
                    SelectCityToAddDropdown.Items.Add(city);

                }

                SelectCityToAddDropdown.SelectedIndex = 0;

            }

        }

        /// \brief This is the function that is triggered when the "accept customer" button is clicked.
        ///
        /// \details It adds the customer selected in the new customer list to the TMS database as an accepted customer.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        ///
        private void AcceptSelectedCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            newOrder.CustomerID = nTMS.AddCustomer(newOrder.CustomerName);
            AcceptedCustomerListBox.Items.Clear();

            foreach (string customer in nTMS.DisplayCustomers())
            {
                AcceptedCustomerListBox.Items.Add(customer);
                Logger.Log("Customer accepted: " + customer);
            }
            NewCustomerListBox.IsEnabled=false;
            CreateNewOrderButton.IsEnabled = true;
            AcceptSelectedCustomerButton.IsEnabled = false;
        }

        /// \brief This is the function that is triggered when the "create new order" button is clicked. 
        ///
        /// \details It stores the order in the TMS database.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        ///
        private void CreateNewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(newOrder.JobType != 1)
            {
                sCities = newOrder.OriginCity + "," + newOrder.DestinationCity;
            }
            newOrder.Id = nTMS.InsertOrder(newOrder);
            nTMS.AvailableCarriers(sCities, newOrder.Id);

            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result = MessageBox.Show("Order Number: " + newOrder.Id + " was added!", "Order Added", button);

            this.Close();
            BuyerMainWindow newWindow = new BuyerMainWindow();
            newWindow.Show();
            Logger.Log("Order Number: " + newOrder.Id + " was added!");

        }

        /// \brief This is the function that is triggered when the "add city to order" button is clicked.  
        ///
        ///  \details It adds the selected city to the list of cities attached to the current order.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        ///
        private void AddCityToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            
            sCities += SelectCityToAddDropdown.SelectedValue.ToString() + ",";
            
            CreateNewOrderButton.IsEnabled = true;

            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxResult result = MessageBox.Show(SelectCityToAddDropdown.SelectedValue.ToString() + " has been added to city list", "City Added", button);
            SelectCityToAddDropdown.Items.Remove(SelectCityToAddDropdown.SelectedValue);
            
            if(SelectCityToAddDropdown.Items.Count > 0)
            {
                SelectCityToAddDropdown.SelectedIndex = 0;
            }
            else
            {
                SelectCityToAddDropdown.IsEnabled= false;
                AddCityToOrderButton.IsEnabled= false;
            }

        }

        private void GenerateInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedOrder = 0;
            selectedOrder = Int32.Parse(CompletedOrdersListBox.SelectedItem.ToString().Split(' ')[0]);
            nTMS.GenerateInvoice(selectedOrder);
            Logger.Log("Invoice generated");
        }
    }
}