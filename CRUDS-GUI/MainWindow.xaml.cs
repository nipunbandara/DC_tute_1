using DatabaseWebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CRUDS_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RestClient restClient;
        static byte[] profilep;

        public MainWindow()
        {
            InitializeComponent();
            string URL = "https://localhost:44369/";
            restClient = new RestClient(URL);
            
           
        }



        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
            RestRequest restRequest = new RestRequest("api/BankingUsers/{id}", Method.Get);
            restRequest.AddUrlSegment("id", SearchBox.Text);
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser user = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            AcctNoBox.Text = user.accNo.ToString();
            FNameBox.Text = user.firstName.ToString();
            LNameBox.Text= user.lastName.ToString();
            PinBox.Text = user.pin.ToString();
            BalanceBox.Text = user.balance.ToString();
            ImageSec.Source = BinaryToImage(user.pictureData);
            profilep = user.pictureData;

        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            BankingUser user = new BankingUser();
            user.accNo = AcctNoBox.Text;
            user.balance = Int32.Parse(BalanceBox.Text);
            user.pin = Int32.Parse(PinBox.Text);
            user.firstName = FNameBox.Text;
            user.lastName = LNameBox.Text;
            user.pictureData = ImageToBinary(FileNameTextBox.Text);


            RestRequest restRequest = new RestRequest("api/BankingUsers", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(user));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            if (returnStudent != null)
            {
                MessageBox.Show("Data Successfully Inserted");
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            BankingUser user = new BankingUser();
            user.accNo = AcctNoBox.Text;
            user.balance = Int32.Parse(BalanceBox.Text);
            user.pin = Int32.Parse(PinBox.Text);
            user.firstName = FNameBox.Text;
            user.lastName = LNameBox.Text;
            user.pictureData = profilep;


            RestRequest restRequest = new RestRequest($"api/BankingUsers/{user.accNo}", Method.Put);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(user));
            RestResponse restResponse = restClient.Execute(restRequest);
            /*var errorMessage = restResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            MessageBox.Show(errorMessage);
            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);*/
            if (restResponse.Content != null)
            {
                MessageBox.Show("Data Successfully Updated");
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
        }

        private void DeleteButton_Click(Object sender, EventArgs e)
        {
            BankingUser user = new BankingUser();
            user.accNo = AcctNoBox.Text;
            


            RestRequest restRequest = new RestRequest($"api/BankingUsers/{user.accNo}", Method.Delete);
            //restRequest.AddJsonBody(JsonConvert.SerializeObject(user));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            if (returnStudent != null)
            {
                MessageBox.Show("Data Successfully Deleted");

                AcctNoBox.Text = "";
                FNameBox.Text = "";
                LNameBox.Text = "";
                PinBox.Text = "";
                BalanceBox.Text = "";
                ImageSec.Source = null;
            }
            else
            {
                MessageBox.Show("Error details:" + restResponse.Content);
            }
        }

        private void BrowsButton_Click(object sender, EventArgs e)
        {

            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".png";
            dlg.Filter = "All files (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                FileNameTextBox.Text = filename;
            }
        }

        public BitmapImage BinaryToImage(byte[] binaryData)
        {
            if (binaryData == null)
                return null;

            var binary = binaryData.ToArray();
            var image = new BitmapImage();
            var ms = new MemoryStream();

            ms.Write(binary, 0, binary.Length);

            image.BeginInit();
            image.StreamSource = new MemoryStream(ms.ToArray());
            image.EndInit();

            return image;
        }

        public byte[] ImageToBinary(String filePath)
        {
            byte[] data = File.ReadAllBytes(filePath);
            return data;

        }
        
    }
}
