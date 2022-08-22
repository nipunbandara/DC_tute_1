using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        int GetNumEntries();
        [OperationContract]
        [FaultContract(typeof(InvalidOperationException))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap profilePic);
    }

    //custom exception handling declaration
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



