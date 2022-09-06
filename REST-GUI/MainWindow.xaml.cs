using RestSharp;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Biz_GUI_Classes;

namespace REST_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        RestClient client;

        public MainWindow()
        {
            InitializeComponent();

            string URL = "https://localhost:44319/";
            client = new RestClient(URL);
            RestRequest request = new RestRequest("api/getvalues");
            RestResponse numOfThings = client.Get(request);
            TotalNum.Text = numOfThings.Content;

        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            //On click, Get the index....
            int index = Int32.Parse(IndexNum.Text);
            //Then, set up and call the API method...
            RestRequest request = new RestRequest("api/getvalues/" + index.ToString());
            RestResponse resp = client.Get(request);
            //And now use the JSON Deserializer to deseralize our object back to the class we want
            DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(resp.Content);
            //And now, set the values in the GUI!
            FNameBox.Text = dataIntermed.firstName;
            LNameBox.Text = dataIntermed.lastName;
            BalanceBox.Text = dataIntermed.balance.ToString("C");
            AcctNoBox.Text = dataIntermed.accNo.ToString();
            PinBox.Text = dataIntermed.pin.ToString("D4");
            ImageSec.Source = BitmapImageConverter(dataIntermed.profilePic);
            
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

        }


        private void SearchButthon_Click(object sender, RoutedEventArgs e)
        {
            //Make a search class
            SearchData mySearch = new SearchData();
            mySearch.searchStr = SearchBox.Text;
            //Build a request with the json in the body
            RestRequest request = new RestRequest("api/search/");
            request.AddJsonBody(mySearch);
            //Do the request
            RestResponse resp = client.Post(request);
            //Deserialize the result
            DataIntermed dataIntermed = JsonConvert.DeserializeObject<DataIntermed>(resp.Content);
            //aaaaand input the data
            FNameBox.Text = dataIntermed.firstName;
            LNameBox.Text = dataIntermed.lastName;
            BalanceBox.Text = dataIntermed.balance.ToString("C");
            AcctNoBox.Text = dataIntermed.accNo.ToString();
            PinBox.Text = dataIntermed.pin.ToString("D4");
            ImageSec.Source = BitmapImageConverter(dataIntermed.profilePic);

        }

        BitmapImage BitmapImageConverter(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();
            return image;
        }


        /* private void SearchButthon_Click(object sender, RoutedEventArgs e)
         {
             string fName = "", lName = "";
             int bal = 0;
             uint acct = 0, pin = 0;
             Bitmap profilePic = null;
             foob.GetValuesForSearch(SearchBox.Text, out acct, out pin, out bal, out fName, out lName, out profilePic);
             FNameBox.Text = fName;
             LNameBox.Text = lName;
             BalanceBox.Text = bal.ToString("C");
             AcctNoBox.Text = acct.ToString();
             PinBox.Text = pin.ToString("D4");
             ImageSec.Source = BitmapToImageSource(profilePic);
         }

         //Bitmap to bitmapImage conversion method
         private ImageSource BitmapToImageSource(Bitmap bitmap)
         {
             var handle = bitmap.GetHbitmap();

             return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
         }*/
    }
}
