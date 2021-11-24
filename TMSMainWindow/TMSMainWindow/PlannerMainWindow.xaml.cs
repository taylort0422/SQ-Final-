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

            }
            AddTripToOrderButton.IsEnabled = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ActiveOrdersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CarrierListBox.Items.Clear();
            TripsAttachedToOrderListBox.Items.Clear();
            if (ActiveOrdersDataGrid.SelectedItems.Count > 0)
            {
                int currOrderID = Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]);

                List<string> carriers = nTMS.DisplayCarriers(currOrderID);
                carriers.Sort();
                for (int i = 0; i < carriers.Count; i++)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Content = carriers[i].Split(':')[1] + "-" + carriers[i].Split(':')[2];
                    checkBox.Name = "Carrier_" + carriers[i].Split(':')[0].Trim();

                    CarrierListBox.Items.Add(checkBox);
                }

                //Check if trips already exist on this order. display in existing trips 
                List<Trip> openTrip = new List<Trip>();
                openTrip = nTMS.ListOfTrips(currOrderID);
                if (openTrip.Count > 0)
                {
                    foreach (Trip trip in openTrip)
                    {
                        TripsAttachedToOrderListBox.Items.Add(nTMS.GetCarrierFromID(trip.OriginDepotID) + " To " + nTMS.GetCarrierFromID(trip.DestinationDepotID));
                    }
                }
            }
            
            
        }

        private void AddTripToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var checkBoxes = CarrierListBox.Items.OfType<CheckBox>();
            int checkedBoxes = 0;
            List<int> selectedElements = new List<int>();
            List<string> cList = new List<string>();
            for(int i =0; i<checkBoxes.Count(); i++)
            {
                if(checkBoxes.ElementAt(i).IsChecked == true)
                {
                    cList.Add(checkBoxes.ElementAt(i).Content.ToString());
                    checkedBoxes++;
                    checkBoxes.ElementAt(i).IsChecked = false;
                    checkBoxes.ElementAt(i).IsEnabled = false;
                    selectedElements.Add(i);
                    //TripsAttachedToOrderListBox.Items.Add(checkBoxes.ElementAt(i).Content.ToString());
                }
            }

            if(checkedBoxes == 2)
            {
                // Get the IDs of the origin and destination
                int originID = nTMS.GetCarrierID(cList.ElementAt(0).Split('-')[0].Trim(), cList.ElementAt(0).Split('-')[1].Trim());

                int destID = nTMS.GetCarrierID(cList.ElementAt(1).Split('-')[0].Trim(), cList.ElementAt(1).Split('-')[1].Trim());

                string trip = (originID + "-" + destID);

                var tripsExist = TripsAttachedToOrderListBox.Items;
                if (tripsExist.Count > 0)
                {
                    for (int c = 0; c < tripsExist.Count; c++)
                    {
                        string tmp = tripsExist[c].ToString();
                        string tmp2 = (cList.ElementAt(0).Trim() + " To " + cList.ElementAt(1).Trim());

                        if (tmp != tmp2)
                        {

                            // Here we have to check if there is enough trucks at the origin depot to perform the operation

                            // Add the trip to the database
                            nTMS.PlanTrip(trip, Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));
                            // Move the trip to trip added list
                            TripsAttachedToOrderListBox.Items.Add(cList.ElementAt(0) + " To " + cList.ElementAt(1));

                        }
                        else
                        {
                            MessageBoxButton button = MessageBoxButton.OK;
                            MessageBoxResult result = MessageBox.Show("This Trip already exists!", "Duplicates", button);
                            for (int i = 0; i < checkBoxes.Count(); i++)
                            {
                                checkBoxes.ElementAt(i).IsChecked = false;
                                checkBoxes.ElementAt(i).IsEnabled = true;
                            }
                        }
                    }
                }
                else if (tripsExist.Count == 0)
                {
                    // Add the trip to the database
                    nTMS.PlanTrip(trip, Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));
                    // Move the trip to trip added list
                    TripsAttachedToOrderListBox.Items.Add(cList.ElementAt(0) + " To " + cList.ElementAt(1));
                }
            }
            else
            {
                selectedElements.Clear();
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("You must select an Origin and a Destination", "Choices!", button);
                for (int i = 0; i < checkBoxes.Count(); i++)
                {
                    checkBoxes.ElementAt(i).IsChecked = false;
                    checkBoxes.ElementAt(i).IsEnabled = true;
                }
            }
            
            AddTripToOrderButton.IsEnabled = false;

        }

        private void MarkOrderAsCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            nTMS.MarkOrderComplete(Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));
            TripsAttachedToOrderListBox.Items.Clear();
            CarrierListBox.Items.Clear();
            ActiveOrdersDataGrid.Items.Clear();
            openOrders = nTMS.ListOpenOrders();
            foreach (Order orders in openOrders)
            {
                ActiveOrdersDataGrid.Items.Add(orders);

            }

        }

        private void CarrierListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTripToOrderButton.IsEnabled = true;
        }
    }
}
