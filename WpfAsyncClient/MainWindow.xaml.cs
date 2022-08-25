using BusinessTier;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataBase;
using System.Runtime.Remoting.Messaging;

namespace WpfAsyncClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public delegate DataStruct Search(string value); //delegate for searching

    public partial class MainWindow : Window
    {
        private BusinessServerInterface foob;
        private string searchvalue;
        private Search search; //reference to method


        public MainWindow()
        {
            InitializeComponent();
            //This is a factory that generates remote connections to our remote class. This is what hides the RPC stuff!
            ChannelFactory<BusinessServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Increasing passable object size
            tcp.MaxReceivedMessageSize = 2147483647;
            tcp.MaxBufferSize = 2147483647;
            tcp.MaxBufferPoolSize = 2147483647;
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/BusinessService";
            try
            {
                foobFactory = new ChannelFactory<BusinessServerInterface>(tcp, URL);
                foob = foobFactory.CreateChannel();
                //Also, tell me how many entries are in the DB.
                TotalNum.Text = foob.GetNumEntries().ToString();
            }
            catch (Exception e)
            {
                MyException m = new MyException();
                MessageBox.Show("Database connection problem!");
                m.Reason = "Database connection problem!";
                throw new FaultException<MyException>(m);

            }
        }
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            Bitmap profilePic = null;
            //On click, Get the index....
            try
            {
                index = int.Parse(IndexNum.Text);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter Valid number !");


                return;
            }
            //Then, run our RPC function, using the out mode parameters...
            try
            {
                //condition to prevent from entering out of index numbers
                if (index > foob.GetNumEntries() || index <= 0)
                {
                    Console.WriteLine("Index out of bound");
                    MessageBox.Show("Enter index within range");


                }
                else
                {
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName, out profilePic);
                }
                FNameBox.Text = fName;
                LNameBox.Text = lName;
                BalanceBox.Text = bal.ToString("C");
                AcctNoBox.Text = acct.ToString();
                PinBox.Text = pin.ToString("D4");
                ImageSec.Source = BitmapToImageSource(profilePic);

            }
            catch
            {
                MyException m = new MyException();
                m.Reason = "Problem occured when passing data to variables";
                throw new FaultException<MyException>(m);
            }



        }
        //Bitmap to bitmapImage conversion method
        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            var handle = bitmap.GetHbitmap();

            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }


        private async void SearchButthon_Click(object sender, RoutedEventArgs e)
        {
            /*            search = SearchDB;
                        AsyncCallback callback;
                        callback = this.OnSearchCompletion;
                        IAsyncResult result = search.BeginInvoke(SearchBox.Text, callback, null);*/
            searchvalue = SearchBox.Text;
            int testSB = 0;
            try 
            {
                testSB = int.Parse(SearchBox.Text);
            }
            catch
            {
                
            }
            

            if (testSB != 0)
            {
                MessageBox.Show("Enter String Values");
                return;
            }

            
            int timeout = 10000;

            Task<DBGenerator> task = new Task<DBGenerator>(SearchDB);
            task.Start();
            statusLabel.Content = "Searching starts.....";
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                // task completed within timeout
                DBGenerator dbgener = await task;
                UpdateGui(dbgener);
                statusLabel.Content = "Searching ends....";
            }
            else
            {
                statusLabel.Content = "Timed out ....";
            }
            
        }

        private DBGenerator SearchDB()
        {
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            Bitmap profilePic = null;
            try 
            { 
                foob.GetValuesForSearch(searchvalue, out acct, out pin, out bal, out fName, out lName, out profilePic); 
            }
            catch (Exception e)
            {
                MessageBox.Show("Name is not available in database !");
                
            }
            

            if (acct != 0)
            {
                DBGenerator aUser = new DBGenerator();
                aUser.acctNo = acct;
                aUser.pin = pin;
                aUser.balance = bal;
                aUser.firstName = fName;
                aUser.lastName = lName;
                aUser.profilePic = profilePic;
                return aUser;
            }
            return null;
        }

        private void UpdateGui(DBGenerator aUser)
        {
            if (aUser == null)
            {
                MessageBox.Show("Name is not available in database !");

            }
            else
            {
                FNameBox.Text = aUser.firstName;
                LNameBox.Text = aUser.lastName;
                BalanceBox.Text = aUser.balance.ToString("C");
                AcctNoBox.Text = aUser.acctNo.ToString();
                PinBox.Text = aUser.pin.ToString("D4");
                ImageSec.Source = BitmapToImageSource(aUser.profilePic);
            }
            
        }

        /*private void OnSearchCompletion(IAsyncResult asyncResult)
        {
            DataStruct iuser = null;
            Search search = null;
            AsyncResult asyncobj = (AsyncResult)asyncResult;
            if (asyncobj.EndInvokeCalled == false)
            {
                search = (Search)asyncobj.AsyncDelegate;
                iuser = search.EndInvoke(asyncobj);
                UpdateGui(iuser);
            }

            asyncobj.AsyncWaitHandle.Close();


        }*/
    }
}
