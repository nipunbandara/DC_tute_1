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
using System.Drawing;
using System.IO;
using Buisness_tier_console_application;
using System.Runtime.Remoting.Messaging;

//**********************This class contains DELEGATION method, use WpfAppAsync to view all tasks************
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate DatabaseModel Search(string val);
        private BuisnessInterface foob;
        private Search search;
        private int tempIndex;

        public MainWindow()
        {
            InitializeComponent();

            ChannelFactory<BuisnessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            
            string URL = "net.tcp://localhost:8200/DataService123";
            foobFactory = new ChannelFactory<BuisnessInterface>(tcp,URL);
            foob = foobFactory.CreateChannel();
            TotalNum.Text = foob.GetNumEntries().ToString();
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {

                int index = 0;
                string fName = "", lName = "";
                int bal = 0;
                uint acct = 0, pin = 0;
                string inPath = "";

            try
            {
                //Parses to integer 32bit
                index = Int32.Parse(Index.Text);
            }
            catch (Exception pl) {

                Console.WriteLine("Error:" + pl);
                MessageBox.Show("OOPS. There is an error: " + pl, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                
                //Retreive the user requested record
                foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out inPath);

            }
            catch(FaultException<MyException> ex) {

                Console.WriteLine("Error:" + ex);
                MessageBox.Show("OOPS. There is an error: "+ ex,"Error",MessageBoxButton.OK, MessageBoxImage.Error);


            }
            //Below code sets the value in the fromend from
                fNameBox.Text = fName;
                lNameBox.Text = lName;
                Balance.Text = bal.ToString("C");
                AcctNoBox.Text = acct.ToString();
                PinBox.Text = pin.ToString("D4");


            //Path of the Image file
            string path = Directory.GetCurrentDirectory() + @"\Profile_photos\" + inPath;

            try
            {
                //Sets the image to the frontend form
                picture123.Source = new BitmapImage(new Uri(path));
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                MessageBox.Show("OOPS. There is an error: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            


        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //int bal = 0, index;
            //uint acct = 0, pin = 0;
            //string fname, lname, inPath;

            //foob.GetValuesForSearch(SearchBox.Text, out index, out bal, out acct, out pin, out fname, out lname, out inPath);
            Progressbar123.IsIndeterminate = true;
            Index.IsReadOnly = true;
            PinBox.IsReadOnly = true;
            AcctNoBox.IsReadOnly = true;
            Balance.IsReadOnly = true;
            fNameBox.IsReadOnly = true;
            lNameBox.IsReadOnly = true;
            SearchBox.IsReadOnly = true;
            goBtn.IsEnabled = false;
            SearchBtn.IsEnabled = false;
            //MessageBox.Show("Your answers are: balance: " + index + "/" + fname, lname);
            search = searchDB;
            AsyncCallback callback;
            callback = this.OnSearchCompletion;
            IAsyncResult result = search.BeginInvoke(SearchBox.Text, callback, null);
        }

        private DatabaseModel searchDB(string val) {

            int bal = 0, index;
            uint acct = 0, pin = 0;
            string fname, lname, inPath;

            foob.GetValuesForSearch(val, out index, out bal, out acct, out pin, out fname, out lname, out inPath);


            DatabaseModel model = new DatabaseModel();
            model.balance = bal;
            model.acctNo = acct;
            model.pin = pin;
            model.firstName = fname;
            model.lastName = lname;
            model.InPath = inPath;
            getIndex(index);
            return model;

        }
        private void UpdateGui(DatabaseModel model) {

            string path = Directory.GetCurrentDirectory() + @"\Profile_photos\" + model.InPath;;
            picture123.Dispatcher.Invoke(new Action(() => picture123.Source = new BitmapImage(new Uri(path))));
            Index.Dispatcher.Invoke(new Action(() => Index.Text = tempIndex.ToString()));
            PinBox.Dispatcher.Invoke(new Action(() => PinBox.Text = model.pin.ToString()));
            AcctNoBox.Dispatcher.Invoke(new Action(() => AcctNoBox.Text = model.acctNo.ToString()));
            Balance.Dispatcher.Invoke(new Action(() => Balance.Text = model.balance.ToString()));
            fNameBox.Dispatcher.Invoke(new Action(() => fNameBox.Text = model.firstName.ToString()));
            lNameBox.Dispatcher.Invoke(new Action(() => lNameBox.Text = model.lastName.ToString()));
            Progressbar123.Dispatcher.Invoke(new Action(() => Progressbar123.IsIndeterminate = false));
            Index.Dispatcher.Invoke(new Action(() => Index.IsReadOnly = false));
            PinBox.Dispatcher.Invoke(new Action(() => PinBox.IsReadOnly = false));
            AcctNoBox.Dispatcher.Invoke(new Action(() => AcctNoBox.IsReadOnly = false));
            Balance.Dispatcher.Invoke(new Action(() => Balance.IsReadOnly = false));
            fNameBox.Dispatcher.Invoke(new Action(() => fNameBox.IsReadOnly = false));
            lNameBox.Dispatcher.Invoke(new Action(() => lNameBox.IsReadOnly = false));
            SearchBox.Dispatcher.Invoke(new Action(() => SearchBox.IsReadOnly = false));
            goBtn.Dispatcher.Invoke(new Action(() => goBtn.IsEnabled = true));
            SearchBtn.Dispatcher.Invoke(new Action(() => SearchBtn.IsEnabled = true));
        }

        private void OnSearchCompletion(IAsyncResult asyncResult) {

            
            Search search = null;
            DatabaseModel model = null;
            AsyncResult asyncobj = (AsyncResult)asyncResult;
            if (asyncobj.EndInvokeCalled == false)
            {
                search = (Search)asyncobj.AsyncDelegate;
                model = search.EndInvoke(asyncobj);
                UpdateGui(model);
            }

            asyncobj.AsyncWaitHandle.Close();
        }

        private void getIndex(int index) {
            tempIndex = index;
        }

        private void Progressbar123_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}