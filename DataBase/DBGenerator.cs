using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    internal class DBGenerator : DataStruct
    {

        private string GetFirstname()
        {
            return firstName;
        }
        private string GetLastname()
        {
            return lastName;
        }
        private uint GetPIN()
        {
            return pin;
        }
        private uint GetAcctNo()
        {
            return acctNo;
        }
        private int GetBalance()
        {
            return balance;
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstname();
            lastName = GetLastname();
            balance = GetBalance();

        }


    }
}
