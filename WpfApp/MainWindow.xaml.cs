using RemoteServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            //Increasing passable object size
            tcp.MaxReceivedMessageSize = 2147483647;
            tcp.MaxBufferSize = 2147483647;
            tcp.MaxBufferPoolSize = 2147483647;
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            try
            {
                foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
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
            catch(Exception ex)
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
                ImageSec.Source = BitmapToBitmapImage(profilePic);

            }
            catch
            {
                MyException m = new MyException();
                m.Reason = "Problem occured when passing data to variables";
                throw new FaultException<MyException>(m);
            }


          
        }

        //Bitmap to bitmapImage conversion method
        private BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
             BitmapImage bitmapImage = new BitmapImage();
             using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
             {
                 bitmap.Save(ms, bitmap.RawFormat);
                 bitmapImage.BeginInit();
                 bitmapImage.StreamSource = ms;
                 bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                 bitmapImage.EndInit();
                 bitmapImage.Freeze();
             }
             return bitmapImage;
        }
    }
}
