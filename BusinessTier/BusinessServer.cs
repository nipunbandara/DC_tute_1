using RemoteServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessTier
{
    internal class BusinessServer : BusinessServerInterface
    {
        private DataServerInterface foob;
        uint LogNumber = 0;
        string logstr = null;

        public BusinessServer()
        {
            ChannelFactory<DataServerInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            tcp.MaxReceivedMessageSize = 2147483647;
            tcp.MaxBufferSize = 2147483647;
            tcp.MaxBufferPoolSize = 2147483647;
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8100/DataService";
            foobFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();

            LogNumber++;
            logstr = "Log Number : " + LogNumber + " Date/Time : " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Log Detail : ";
            Log(logstr + "Server Initialized\n");
        }

        public int GetNumEntries()
        {
            LogNumber++;
            logstr = "Log Number : " + LogNumber + " Date/Time : " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Log Detail : ";
            Log(logstr + "GetNumEntries Function Executed\n");
            return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap profilePic)
        {
            LogNumber++;
            logstr = "Log Number : " + LogNumber + " Date/Time : " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Log Detail : ";
            Log(logstr + "GetValuesForEntry function Executed\n");
            foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out profilePic);
        }

        public void GetValuesForSearch(string searchText, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap profilePic)
        {
            LogNumber++;
            logstr = "Log Number : " + LogNumber + " Date/Time : " + System.DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " Log Detail : ";
            Log(logstr + "GetValuesForSearch function Executed\n");

            acctNo = 0;
            pin = 0;
            bal = 0;
            fName = null;
            lName = null;
            profilePic = null;
            int numEntry = foob.GetNumEntries();
            for (int i = 1; i <= numEntry; i++)
            {
                uint aNo;
                uint pn;
                int bl;
                string fn;
                string ln;
                Bitmap prp;

                foob.GetValuesForEntry(i, out aNo, out pn, out bl, out fn, out ln, out prp);
                if (fn.ToLower().Contains(searchText.ToLower()))
                {
                    acctNo = aNo;
                    pin = pn;
                    bal = bl;
                    fName = fn;
                    lName = ln;
                    profilePic = prp;
                    break;
                }
                
            }

            Thread.Sleep(5000); //Forced sleep for two seconds
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void Log(string logString)
        {
            lock (this)
            {
                string path = @"C:\resources\log.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(logString);
                        //sw.Close();
                    }

                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(logString);

                }

            }
        }
    }
}
