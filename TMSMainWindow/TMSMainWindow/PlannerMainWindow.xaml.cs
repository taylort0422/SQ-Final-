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
        /// A private variable.
        ///
        /// This variable stores the communication information for the local database.
        ///
        private CommTMS nTMS = new CommTMS(ConfigurationManager.AppSettings.Get("localUser"),
                        ConfigurationManager.AppSettings.Get("localPass"),
                        ConfigurationManager.AppSettings.Get("localIP"),
                        Int32.Parse(ConfigurationManager.AppSettings.Get("localPort")),
                        ConfigurationManager.AppSettings.Get("localDb"));


        /// A function variable.
        ///
        /// This is a list that will be filled with currently open orders.
        ///
        List<Order> openOrders = new List<Order>();

        /// This is the function for the Planner window. It fills the active orders data grid with information from the TMS database.
        
        public PlannerMainWindow()
        {
            InitializeComponent();
            openOrders = nTMS.ListOpenOrders();
            foreach (Order orders in openOrders)
            {
                ActiveOrdersDataGrid.Items.Add(orders);

            }

        }

        /// This is the function that is triggered when an active order is selected from the active orders data grid.
        ///
        /// It updates the relevant WPF controls with information about the selected order.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        
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
                        if (trip.OriginCity == "Windsor" || trip.DestinationCity == "Windsor")
                        {
                            Windsor.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "London" || trip.DestinationCity == "London")
                        {
                            London.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Hamilton" || trip.DestinationCity == "Hamilton")
                        {
                            Hamilton.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Oshawa" || trip.DestinationCity == "Oshawa")
                        {
                            Oshawa.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Toronto" || trip.DestinationCity == "Toronto")
                        {
                            Toronto.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Kingston" || trip.DestinationCity == "Kingston")
                        {
                            Kingston.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Ottawa" || trip.DestinationCity == "Ottawa")
                        {
                            Ottawa.Visibility = Visibility.Visible;
                        }
                        if (trip.OriginCity == "Belleville" || trip.DestinationCity == "Belleville")
                        {
                            Belleville.Visibility = Visibility.Visible;
                        }
                    }
                }
            }


        }

        /// This is the function that is triggered when the "add trip to order" button is clicked.
        ///
        /// It adds the selected trip to the current Order.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        
        private void AddTripToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(ActiveOrdersDataGrid.SelectedItems.Count > 0)
            {
                int orderID = Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]);
                int orderSize = nTMS.GetLoad(orderID);
                var checkBoxes = CarrierListBox.Items.OfType<CheckBox>();
                int checkedBoxes = 0;
                List<int> selectedElements = new List<int>();
                List<string> cList = new List<string>();
                for (int i = 0; i < checkBoxes.Count(); i++)
                {
                    if (checkBoxes.ElementAt(i).IsChecked == true)
                    {
                        cList.Add(checkBoxes.ElementAt(i).Content.ToString());
                        checkedBoxes++;
                        checkBoxes.ElementAt(i).IsChecked = false;
                        checkBoxes.ElementAt(i).IsEnabled = false;
                        selectedElements.Add(i);
                    }

                    
                }

                if (checkedBoxes == 2)
                {
                    // Get the IDs of the origin and destination
                    int originID = nTMS.GetCarrierID(cList.ElementAt(0).Split('-')[0].Trim(), cList.ElementAt(0).Split('-')[1].Trim());

                    int destID = nTMS.GetCarrierID(cList.ElementAt(1).Split('-')[0].Trim(), cList.ElementAt(1).Split('-')[1].Trim());

                    for(int i = 0; i < selectedElements.Count;i++)
                    {
                        string city = cList.ElementAt(i).Split('-')[1].Trim();
                        if(city == "Windsor")
                        {
                            Windsor.Visibility = Visibility.Visible;
                        }
                        if (city == "London")
                        {
                            London.Visibility = Visibility.Visible;
                        }
                        if (city == "Hamilton")
                        {
                            Hamilton.Visibility = Visibility.Visible;
                        }
                        if (city == "Oshawa")
                        {
                            Oshawa.Visibility = Visibility.Visible;
                        }
                        if (city == "Toronto")
                        {
                            Toronto.Visibility = Visibility.Visible;
                        }
                        if (city == "Kingston")
                        {
                            Kingston.Visibility = Visibility.Visible;
                        }
                        if (city == "Ottawa")
                        {
                            Ottawa.Visibility = Visibility.Visible;
                        }
                        if (city == "Belleville")
                        {
                            Belleville.Visibility = Visibility.Visible;
                        }
                    }

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
                                if (nTMS.RemoveTrucks(originID, orderSize) == -1)
                                {
                                    //if this executes, the depot does not have enough trucks to fulfill the order

                                }
                                // Add the trip to the database
                                nTMS.PlanTrip(trip, orderID);
                                // Move the trip to trip added list
                                TripsAttachedToOrderListBox.Items.Add(cList.ElementAt(0) + " To " + cList.ElementAt(1));
                                Logger.Log("Trip" + trip + " added to order");
                                break;
                                

                            }
                            else
                            {
                                MessageBox.Show("This Trip already exists!", "Duplicates");
                                for (int i = 0; i < checkBoxes.Count(); i++)
                                {
                                    checkBoxes.ElementAt(i).IsChecked = false;
                                    checkBoxes.ElementAt(i).IsEnabled = true;
                                }
                                break;
                            }
                        }
                    }
                    else if (tripsExist.Count == 0)
                    {
                        // Add the trip to the database
                        nTMS.PlanTrip(trip, Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));
                        // Move the trip to trip added list
                        TripsAttachedToOrderListBox.Items.Add(cList.ElementAt(0) + " To " + cList.ElementAt(1));
                        if (nTMS.RemoveTrucks(originID, orderSize) == -1)
                        {
                            //if this executes, the depot does not have enough trucks to fulfill the order

                        }
                    }
                }
                else
                {
                    selectedElements.Clear();
                    MessageBox.Show("You must select an Origin and a Destination", "Choices!");

                    for (int i = 0; i < checkBoxes.Count(); i++)
                    {
                        checkBoxes.ElementAt(i).IsChecked = false;
                        checkBoxes.ElementAt(i).IsEnabled = true;
                    }
                }

                AddTripToOrderButton.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Select Trips", "No trip");
            }

        }

        /// This is the function that is triggered when the "mark order as complete" button is clicked.
        ///
        /// It marks the order as complete, clears the relevant wpf controls, and updates the list of active orders.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        
        private void MarkOrderAsCompleteButton_Click(object sender, RoutedEventArgs e)
        {
            nTMS.MarkOrderComplete(Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]));
            TripsAttachedToOrderListBox.Items.Clear();
            CarrierListBox.Items.Clear();
            ActiveOrdersDataGrid.Items.Clear();
            openOrders = nTMS.ListOpenOrders();
            Logger.Log("Order completed");
            foreach (Order orders in openOrders)
            {
                ActiveOrdersDataGrid.Items.Add(orders);
            }

        }

        /// This is the function that is triggered when an item is selected in the Carrier list box.
        ///
        /// It enables the "add trip to order" button.
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        
        private void CarrierListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddTripToOrderButton.IsEnabled = true;
        }

        /// This is the function that is triggered when an item is selected in the Carrier list box.
        ///
        /// It enables the "add trip to order" button.
        /// \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void


        /// This is the function that is used to pass time when the planner selects the "pass time" option.
        ///
        /// It gets the number of days to pass from the UI, then simulates that much time passing.
        /// note: this function has not been implemented yet and is subject to change
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void

        private void PassTimeButton_Click(object sender, RoutedEventArgs e)
        {
            int days = Int32.Parse(NumberOfDaysToPassIntegerUpDown.Value.ToString());
            int hrsPassed = days * 24;
            int orderID = Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]);
            
            DateTime currTime = DateTime.Now;

            DateTime newTime = currTime.AddHours(hrsPassed);

            bool orderCompleted = false;

            orderCompleted = nTMS.ForwardTrip(orderID, newTime);

            if(orderCompleted)
            {
                MarkOrderAsCompleteButton.IsEnabled = true;
            }
            //check if the time forwarding will put us past the final time needed to complete the trip.

        }

        private void RemoveTripFromOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the order ID of the currently selected order
            int orderID = Int32.Parse(ActiveOrdersDataGrid.SelectedItem.ToString().Split(' ')[0]);
            // Get a list of trips in that order
            List<Trip> listTrips = nTMS.ListOfTrips(orderID);

            string selectedTrip = TripsAttachedToOrderListBox.SelectedItem.ToString();

            int pos = selectedTrip.IndexOf("To");

            string origin = selectedTrip.Substring(0, pos).Trim();
            string destination = selectedTrip.Substring(pos + 1).Trim();
           
            origin = origin.Split('-')[1].Trim();
            destination = destination.Split('-')[1].Trim();
    
            var fndTrip = listTrips.FirstOrDefault(i => i.OriginCity == origin && i.DestinationCity == destination);
            if(fndTrip != null)
            {
                nTMS.RemoveTrip(fndTrip.ID);
                TripsAttachedToOrderListBox.Items.Remove(TripsAttachedToOrderListBox.SelectedItem);
 
                if (origin == "Windsor" || destination == "Windsor")
                {
                    Windsor.Visibility = Visibility.Collapsed;
                }
                if (origin == "London" || destination == "London")
                {
                    London.Visibility = Visibility.Collapsed;
                }
                if (origin == "Hamilton" || destination == "Hamilton")
                {
                    Hamilton.Visibility = Visibility.Collapsed;
                }
                if (origin == "Oshawa" || destination == "Oshawa")
                {
                    Oshawa.Visibility = Visibility.Collapsed;
                }
                if (origin == "Toronto" || destination == "Toronto")
                {
                    Toronto.Visibility = Visibility.Collapsed;
                }
                if (origin == "Kingston" || destination == "Kingston")
                {
                    Kingston.Visibility = Visibility.Collapsed;
                }
                if (origin == "Ottawa" || destination == "Ottawa")
                {
                    Ottawa.Visibility = Visibility.Collapsed;
                }
                if (origin == "Belleville" || destination == "Belleville")
                {
                    Belleville.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxResult result = MessageBox.Show("Trip Not Found!", "Choices!", button);
            }
            // Get the IDs of the origin and destination
            //int originID = nTMS.GetCarrierID(cList.ElementAt(0).Split('-')[0].Trim(), cList.ElementAt(0).Split('-')[1].Trim());

            //int destID = nTMS.GetCarrierID(cList.ElementAt(1).Split('-')[0].Trim(), cList.ElementAt(1).Split('-')[1].Trim());

        }

        private void GenerateInvoiceAllTimeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Report saved to: " + nTMS.GenerateReport(DateTime.Now.AddDays(-1)), "Choices!");
        }

        private void GenerateInvoicePast2WeeksButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Report saved to: " + nTMS.GenerateReport(DateTime.Now.AddDays(-14)), "Choices!");
        }
    }
}
