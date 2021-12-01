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
using Newtonsoft.Json;

namespace TMSMainWindow
{



    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
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

        



        /// \brief This is the function for the Admin window. It fills the relevant WPF controls with data from the external and internal databases.
        
        public AdminMainWindow()
        {
            InitializeComponent();
            CurrentLogFileDirectoryTextBox.Text = ConfigurationManager.AppSettings.Get("logPath");
            // Generate the carrier list to display the details
            List<Carrier> cList = new List<Carrier>();
            cList = nTMS.CarrierList();
            CarrierDataGrid.ItemsSource = cList;

            // Generate route table to display
            List<Route> results = JsonConvert.DeserializeObject<List<Route>>(nTMS.GetRouteTable());
            RouteDataGrid.ItemsSource = results;

            // Display configuration data
            LocalDatabaseIPAddressTextBox.Text = ConfigurationManager.AppSettings.Get("localIP");
            LocalDatabasePortNumberTextBox.Text = ConfigurationManager.AppSettings.Get("localPort");
            ContractIPAddressTextBox.Text = ConfigurationManager.AppSettings.Get("marketplaceIP");
            ContractPortNumberTextBox.Text = ConfigurationManager.AppSettings.Get("marketplacePort");
        }

        /// \brief This is the function that is used to change the currently displayed logfile.
        ///
        /// \details It gets the new logfile location from the UI, then updates the UI to show the contents of the new logfile.
        /// note: this function has not been implemented yet and is subject to change
        ///  \param sender the wpf object that called the function.
        ///  \param e the arguments of the event that was triggered.
        ///  \return void
        
        private void ChangeLogFile(object sender, SelectionChangedEventArgs e)
        {

        }


        private void SaveRouteTableChangesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Carrier> carrierList = CarrierDataGrid.Items.OfType<Carrier>().ToList();
            foreach (Carrier c in carrierList)
            {
                nTMS.UpDateCarrier(c);
            }
        }

        private void SaveCarrierDataChangesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Route> routeList = RouteDataGrid.Items.OfType<Route>().ToList();
            foreach (Route r in routeList)
            {
                nTMS.UpDateRouteTable(r);
            }
        }
    }
}
