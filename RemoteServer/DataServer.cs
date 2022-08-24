﻿using System;
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
            //try
            //{
                DatabaseClass users = new DatabaseClass();
                return users.GetNumRecords();
           // }
            /*catch (Exception e)
            {
                MyException m = new MyException();
                m.Reason = "Problem occured when calling database class getNumRecords method";
                throw new FaultException<MyException>(m);
            }*/
        }
        
        public void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap profilePic)
        {
            try
            {
                DatabaseClass users = new DatabaseClass();
                acctNo = users.GetAcctNoByIndex(index);
                pin = users.GetPINByIndex(index);
                bal = users.GetBalanceByIndex(index);
                fName = users.GetFirstNameByIndex(index);
                lName = users.GetLastNameByIndex(index);
                profilePic = users.GetProfilePicByIndex(index);
            }

            catch (Exception e)
            {
                MyException m = new MyException();
                m.Reason = "Problem occured when assigning data to variables";
                throw new FaultException<MyException>(m);
               
            }

        }

        

    }
}