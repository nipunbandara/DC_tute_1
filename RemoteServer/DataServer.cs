using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataBase;

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
            DatabaseClass users = new DatabaseClass();
            return users.GetNumRecords();
        }
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName)
        {
            DatabaseClass users = new DatabaseClass();
            acctNo = users.GetAcctNoByIndex(index);
            pin = users.GetPINByIndex(index);
            bal = users.GetBalanceByIndex(index);
            fName = users.GetFirstNameByIndex(index);
            lName = users.GetLastNameByIndex(index);
        }
        
    }
}