using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class DatabaseClass
    {
        List<DataStruct> dataStruct;

        public DatabaseClass()
        {
            dataStruct = new List<DataStruct>();

            DBGenerator user1 = new DBGenerator();
            user1.firstName = "Gman1";
            user1.lastName = "BlaBla1";
            user1.acctNo = 561651;
            user1.balance = 100000;
            user1.pin = 1111;


            DBGenerator user2 = new DBGenerator();
            user2.firstName = "Gman2";
            user2.lastName = "BlaBla2";
            user2.acctNo = 561652;
            user2.balance = 200000;
            user2.pin = 2222;

            DBGenerator user3 = new DBGenerator();
            user3.firstName = "Gman3";
            user3.lastName = "BlaBla3";
            user3.acctNo = 561653;
            user3.balance = 300000;
            user3.pin = 3333;

            dataStruct.Add(user1);
            dataStruct.Add(user2);
            dataStruct.Add(user3);

        }

        public uint GetAcctNoByIndex(int index)
        {
            return dataStruct[index -1].acctNo;
        }

        public uint GetPINByIndex(int index)
        {
            return dataStruct[index -1].pin;
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

    }
}

