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
using System.Windows.Shapes;
using System.Configuration;

namespace TMSMainWindow
{
    /// <summary>
    /// Interaction logic for PlannerMainWindow.xaml
    /// </summary>
    public partial class PlannerMainWindow : Window
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
        Trip trip = new Trip();
        List<Order> openOrders = new List<Order>();
        
        public PlannerMainWindow()
        {
            InitializeComponent();
            openOrders = nTMS.ListOpenOrders();
            foreach (Order orders in openOrders)
            {
                ActiveOrdersDataGrid.Items.Add(orders);
                //Console.WriteLine(orders.ToString());

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActiveOrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarrierListBox.Items.Clear();
            List<string> carriers = nTMS.DisplayCarriers(Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));

            for (int i = 0; i < carriers.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = carriers[i].Split(':')[1] + "-" + carriers[i].Split(':')[2];
                checkBox.Name = "Carrier_"+ carriers[i].Split(':')[0].Trim();

                CarrierListBox.Items.Add(checkBox);
            }
        }

        private void AddTripToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int firstID = CarrierListBox.SelectedItems.Count;
            //nTMS.PlanTrip()
        }
    }
}
