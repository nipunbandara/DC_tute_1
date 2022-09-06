using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DataList
    { //declaring list data structure for to store data
        List<DataStruct> dataStruct;

        public DataList()
        {
            dataStruct = new List<DataStruct>();

            string[] firstNames = { "Maneesa", "Chathusska", "Rondimal", "Nuduja", "Kalpani", "Sadeevya", "Seniya", "Siwmini", "Chamath", "Shas" };
            string[] lastNames = { "Kanapita", "Rodriguu", "Fernando", "MapaArachchi", "Amaratunga", "Punch", "Bandara", "Karavota", "Palihepitiya", "Gunasinghe" };
            //Using Random to get random numbers
            Random random = new Random(1);
            int imageId = 0;
            string path = "";

            //loop to generate 100 users with random data
            for (int i = 0; i < 10; i++)
            {
                DataStruct account = new DataStruct();
                account.firstName = firstNames[random.Next(0, 10)];
                account.lastName = lastNames[random.Next(0, 10)];
                account.balance = random.Next(0, 10000000);
                account.pin = (uint)random.Next(1000, 9999);
                account.accNo = (uint)random.Next(10000,9999999 );
                imageId = random.Next(1, 5);
                path = @"C:\resources\" + imageId + ".png";
                account.profilePic = path;
                dataStruct.Add(account);


            }

        }


        public List<DataStruct> GetDataList()
        {
            return dataStruct;
        }

        private Bitmap bitmapConvertion(string path)
        {

            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(path, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;

        }

        public uint GetAcctNoByIndex(int index)
        {

            return dataStruct[index - 1].accNo;
        }

        public uint GetPINByIndex(int index)
        {
            return dataStruct[index - 1].pin;
        }
        public string GetFirstNameByIndex(int index)
        {
            return dataStruct[index - 1].firstName;
        }
        public string GetLastNameByIndex(int index)
        {
            return dataStruct[index - 1].lastName;
        }
        public int GetBalanceByIndex(int index)
        {
            return dataStruct[index - 1].balance;
        }
        public int GetNumRecords()
        {
            return dataStruct.Count;
        }
        public string GetProfilePicByIndex(int index)
        {
            return dataStruct[index - 1].profilePic;
        }



    }

}