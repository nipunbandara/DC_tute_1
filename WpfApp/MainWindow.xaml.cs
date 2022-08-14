using RemoteServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataServerInterface foob;

        public MainWindow()
        {
            InitializeComponent();

            //This is a factory that generates remote connections to our remote class. This is what hides the RPC stuff!
            ChannelFactory<DataServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            //Also, tell me how many entries are in the DB.
            TotalNum.Text = foob.GetNumEntries().ToString();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            //On click, Get the index....
            index = int.Parse(IndexNum.Text);
            //Then, run our RPC function, using the out mode parameters...
            foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
            //And now, set the values in the GUI!
            FNameBox.Text = fName;
            LNameBox.Text = lName;
            BalanceBox.Text = bal.ToString("C");
            AcctNoBox.Text = acct.ToString();
            PinBox.Text = pin.ToString("D4");
        }

        private void PinBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
