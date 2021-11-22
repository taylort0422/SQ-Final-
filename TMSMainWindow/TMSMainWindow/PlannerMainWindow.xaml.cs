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
        public PlannerMainWindow()
        {
            InitializeComponent();
            foreach (Order orders in nTMS.ListOpenOrders())
            {
                ActiveOrdersDataGrid.Items.Add(orders);
                //Console.WriteLine(orders.ToString());

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
