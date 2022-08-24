using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DBGenerator : DataStruct
    {

        public string GetFirstname
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string GetLastname
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public uint GetPIN
        {
            get { return pin; }
            set { pin = value; }
        }
        public uint GetAcctNo
        {
            get { return acctNo; }
            set { acctNo = value; }
        }
        public int GetBalance
        {
            get { return balance; }
            set { balance = value; }
        }
        public Bitmap GetProfilePic
        {
            get { return profilePic; }
            set { profilePic = value; }
        }
        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN;
            acctNo = GetAcctNo;
            firstName = GetFirstname;
            lastName = GetLastname;
            balance = GetBalance;

        }


    }
}
