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
using System.Windows.Forms;
using System.IO;

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
        


        private void SaveRouteTableChangesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Route> routeList = RouteDataGrid.Items.OfType<Route>().ToList();
            foreach (Route r in routeList)
            {
                nTMS.UpDateRouteTable(r);
            }
            System.Windows.Forms.MessageBox.Show("Route Table Updated!");
        }

        private void SaveCarrierDataChangesButton_Click(object sender, RoutedEventArgs e)
        {

            List<Carrier> carrierList = CarrierDataGrid.Items.OfType<Carrier>().ToList();
            foreach (Carrier c in carrierList)
            {
                nTMS.UpDateCarrier(c);
            }
            System.Windows.Forms.MessageBox.Show("Carrier Data Updated!");

        }

        private void ChangeBackupDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (ConfigurationManager.AppSettings["localDbDirectory"] != null)
            {
                folderBrowserDialog.SelectedPath = ConfigurationManager.AppSettings["localDbDirectory"];
            }
            DialogResult selectResult = folderBrowserDialog.ShowDialog();

            if (selectResult == System.Windows.Forms.DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string newBackupDirectory = folderBrowserDialog.SelectedPath;
                config.AppSettings.Settings["localDbDirectory"].Value = newBackupDirectory;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                BackupDirectoryTextBox.Text = newBackupDirectory;
                Logger.Log("Database directory changed to: " + newBackupDirectory);
            }

            System.Windows.Forms.MessageBox.Show("Backup Directory Changed!");
        }

        private void BackupLocalDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            nTMS.BackupDB(ConfigurationManager.AppSettings.Get("localDbDirectory"));

            Logger.Log("SQL database backed up");
            System.Windows.Forms.MessageBox.Show("Local Database Backup created!");
        }

        private void ChangeContractConnectionInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["marketplaceIP"].Value = ContractIPAddressTextBox.Text;
            config.AppSettings.Settings["marketplacePort"].Value = ContractPortNumberTextBox.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            Logger.Log("Connection info for remote database changed");
            System.Windows.Forms.MessageBox.Show("Contract database connection info changed!");
        }

        private void ChangeLocalDatabaseConnectionInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["localIP"].Value = LocalDatabaseIPAddressTextBox.Text;
            config.AppSettings.Settings["localPort"].Value = LocalDatabasePortNumberTextBox.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Logger.Log("Connection info for local database changed");
            System.Windows.Forms.MessageBox.Show("Local database connection info changed!");
        }

        private void ChangeLogfileDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (ConfigurationManager.AppSettings["logPath"] != null)
            {
                folderBrowserDialog.SelectedPath = ConfigurationManager.AppSettings["logPath"];
            }
            DialogResult selectResult = folderBrowserDialog.ShowDialog();

            if (selectResult == System.Windows.Forms.DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string newLogDirectory = folderBrowserDialog.SelectedPath;
                config.AppSettings.Settings["logPath"].Value = newLogDirectory;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                CurrentLogFileDirectoryTextBox.Text = newLogDirectory;
                Logger.Log("Log file directory changed to: " + newLogDirectory);
            }
            System.Windows.Forms.MessageBox.Show("Log file Directory Changed!");
        }

        private void ChangeDisplayedLogfileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ConfigurationManager.AppSettings["logPath"];
            DialogResult selectResult = openFileDialog.ShowDialog();

            if (selectResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    CurrentLogfileLabel.Content = "Current logfile: " + openFileDialog.FileName;
                    CurrentLogFileDisplayTextBox.Text = File.ReadAllText(openFileDialog.FileName); //probably incorrect
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Error opening selected logfile.");
                }
            }
        }

    }
}
