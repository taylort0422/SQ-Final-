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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BuyerMainWindow : Window
    {
        private CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                                ConfigurationManager.AppSettings.Get("localPass"),
                                ConfigurationManager.AppSettings.Get("localIP"),
                                Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                                ConfigurationManager.AppSettings.Get("localDb"));

        private Communicate nc = new Communicate(ConfigurationManager.AppSettings.Get("marketplaceUser"),
                                    ConfigurationManager.AppSettings.Get("marketplacePass"),
                                    ConfigurationManager.AppSettings.Get("marketplaceIP"),
                                    Int32.Parse(ConfigurationManager.AppSettings.Get("marketplacePort")),
                                    ConfigurationManager.AppSettings.Get("marketplaceDb"));
        private Order newOrder;
        string sCities = "";
        List<Contract> contracts = new List<Contract>();
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
        }

        private void MarketplaceOrdersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedContract;
            // Get the ID of the selected Order
            string choice = MarketplaceOrdersListBox.SelectedItem.ToString().Split(' ')[0];
            selectedContract = Int32.Parse(MarketplaceOrdersListBox.SelectedItem.ToString().Split(' ')[0]);
            AddCityToOrderButton.IsEnabled = true;

            //Begin creating the new order

            newOrder = new Order(contracts[selectedContract].Name, contracts[selectedContract].Origin, contracts[selectedContract].Destination, contracts[selectedContract].JobType, contracts[selectedContract].VanType);

            if (nTMS.CheckCustomer(newOrder.CustomerName) == 0)
            {
                NewCustomerListBox.Items.Add(newOrder.CustomerName);
            }

            MarketplaceOrdersListBox.IsEnabled = false;
            // 0 = FTL 1 = LTL
            if(newOrder.JobType == 0)
            {
                SelectCityToAddDropdown.IsEnabled = false;
                AddCityToOrderButton.IsEnabled = false;
                CreateNewOrderButton.IsEnabled = true;
            }
            else
            {
                //populate cities to add
                foreach(string city in nTMS.CitiesBetween(newOrder.OriginCity, newOrder.DestinationCity, "East"))
                {
                    SelectCityToAddDropdown.Items.Add(city);
                }
                
            }
    

        }

        private void NewCustomerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AcceptSelectedCustomerButton.IsEnabled = true;
            
            
        }

        private void AcceptSelectedCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            newOrder.CustomerID = nTMS.AddCustomer(newOrder.CustomerName);
            AcceptedCustomerListBox.Items.Clear();
            foreach (string customer in nTMS.DisplayCustomers())
            {
                AcceptedCustomerListBox.Items.Add(customer);
            }
        }

        private void CreateNewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            nTMS.InsertOrder(newOrder);
        }

        private void AddCityToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            sCities += SelectCityToAddDropdown.SelectedValue.ToString() + ",";
        }
    }
}