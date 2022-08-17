using RemoteServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            //On click, Get the index....
            try
            {
                index = int.Parse(IndexNum.Text);

            }
            catch(Exception ex)
            {
                /*MyException m = new MyException();
                m.Reason = "Some Problemo";
                FaultException fe = new FaultException<MyException>(m);
                fe.CreateMessageFault()*/
                MessageBox.Show("Enter Valid number !");

                //throw new FaultException<MyException>(m);
                return;
            }
            //Then, run our RPC function, using the out mode parameters...
            try
            {
                if (index > foob.GetNumEntries() || index <= 0)
                {
                    Console.WriteLine("Index out of bound");
                    MessageBox.Show("Enter index within range");


                }
                else
                {
                    foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
                }
                FNameBox.Text = fName;
                LNameBox.Text = lName;
                BalanceBox.Text = bal.ToString("C");
                AcctNoBox.Text = acct.ToString();
                PinBox.Text = pin.ToString("D4");

            }
            catch
            {
                MyException m = new MyException();
                m.Reason = "Some Problemo";
                throw new FaultException<MyException>(m);
            }
            //foob.GetValuesForEntry(index, out acct, out pin, out bal, out fName, out lName);
            //And now, set the values in the GUI!
            //FNameBox.Text = fName;
            //LNameBox.Text = lName;
            //BalanceBox.Text = bal.ToString("C");
            //AcctNoBox.Text = acct.ToString();
            //PinBox.Text = pin.ToString("D4");



           /* BasicHttpBinding binding = new BasicHttpBinding();

            binding.MaxReceivedMessageSize = 1000000;*/

            //byte[] bytimage = foob.GetImage();

            //Bitmap bitmpimage = Base64StringToBitmap(image);


            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resources\viking.png";
            //BitmapImage vimage = new BitmapImage();
            //vimage.BeginInit();
            //vimage.UriSource = new Uri(path);
            //vimage.EndInit();
            //Console.WriteLine("imagdsf" + image.GetType());
           /* string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\resources\viking.png";
            Bitmap image = new Bitmap(path, true);*/

           /* ImageConverter converter = new ImageConverter();
            Bitmap image = (Bitmap)converter.ConvertFrom(bytimage);

            BitmapImage vimage = BitmapToBitmapImage(image);
            ImageV.Source = vimage;*/
            //ImageV.Source = image;


          
        }

        /*Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;


            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);


            memoryStream.Position = 0;


            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);


            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;


            return bmpReturn;
        }
*/

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
