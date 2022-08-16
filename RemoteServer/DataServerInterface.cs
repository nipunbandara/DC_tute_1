using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace RemoteServer
{
    [ServiceContract]
    public interface DataServerInterface
    {
        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        int GetNumEntries();
        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
        [OperationContract]
        Bitmap GetImage();
    }

    [DataContract]
    public class MyException
    {
        private string strReason;
        public string Reason
        {
            get { return strReason; }
            set { Reason = value; }
        }


    }
}
