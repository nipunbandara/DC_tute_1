using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataBase;
using System.Drawing;

namespace RemoteServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class DataServer : DataServerInterface
    {
        public DataServer()
        {
            //DatabaseClass users = new DatabaseClass();
        }
        public int GetNumEntries()
        {
            try
            {
                DatabaseClass users = new DatabaseClass();
                return users.GetNumRecords();
            }
            catch (Exception e)
            {
                MyException m = new MyException();
                m.Reason = "Some Problemo";
                throw new FaultException<MyException>(m);
            }
        }
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            try
            {
                DatabaseClass users = new DatabaseClass();
                acctNo = users.GetAcctNoByIndex(index);
                pin = users.GetPINByIndex(index);
                bal = users.GetBalanceByIndex(index);
                fName = users.GetFirstNameByIndex(index);
                lName = users.GetLastNameByIndex(index);
            }

            catch (Exception e)
            {
                MyException m = new MyException();
                m.Reason = "Some Problemo";
                throw new FaultException<MyException>(m);
            }

        }

        public Bitmap GetImage()
        {
            DatabaseClass users = new DatabaseClass();
            return users.GetImage();
        }

    }
}