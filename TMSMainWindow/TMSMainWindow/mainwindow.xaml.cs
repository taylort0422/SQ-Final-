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
using System.IO;

namespace TMSMainWindow
{
    /// <summary>
    /// Interaction logic for RoleSelectWindow.xaml
    /// </summary>
    public partial class RoleSelectWindow : Window
    {
        public RoleSelectWindow()
        {
            InitializeComponent();
            try
            {
                string logPath = ConfigurationManager.AppSettings.Get("logPath").ToString();

                if (logPath != "NULL")
                {
                    SetSetting("logPath", logPath);
                    File.Create(logPath).Close();
                }
                else 
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory; 
                    SetSetting("logPath", logPath);
                    File.Create(logPath).Close();
                }
            }
            catch (Exception e)
            {
                Logger.Log("Exception: " + e.Message);
            }

            try
            {
                string dbPath = ConfigurationManager.AppSettings.Get("localDbDirectory");
                if (dbPath == "NULL")
                {
                    dbPath = AppDomain.CurrentDomain.BaseDirectory + "//DBBackup";
                    SetSetting("localDbDirectory", dbPath);
                    Directory.CreateDirectory(dbPath);
                }
            }
            catch(Exception db)
            {
                MessageBox.Show("Exception : " + db.Message);
            }

        }
        private static void SetSetting(string key, string value)
        {
            Configuration configuration =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
       
        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMainWindow adminWindow = new AdminMainWindow();
            adminWindow.Show();
        }

        private void buyerButton_Click(object sender, RoutedEventArgs e)
        {
            BuyerMainWindow buyerWindow = new BuyerMainWindow();
            buyerWindow.Show();
        }

        private void plannerButton_Click(object sender, RoutedEventArgs e)
        {
            PlannerMainWindow plannerWindow = new PlannerMainWindow();
            plannerWindow.Show();
        }
    }
}
