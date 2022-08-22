using RemoteServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    internal class BusinessServer : BusinessServerInterface
    {
        private DataServerInterface foob;

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
        }

        public int GetNumEntries()
        {
          return foob.GetNumEntries();
        }

        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap profilePic)
        {
                foob.GetValuesForEntry(index, out acctNo, out pin, out bal, out fName, out lName, out profilePic);
        }
    }
}
