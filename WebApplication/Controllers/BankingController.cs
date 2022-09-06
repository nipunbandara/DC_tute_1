using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BankingController : ApiController
    {
        public IEnumerable<DataStruct> GetDataStructs()
        {
            DataList list = new DataList();
            return list.GetDataList();
        }

        public IHttpActionResult GetUserByAccNo(int id)
        {
            DataList list = new DataList();
            DataStruct dataStruct = new DataStruct();
            dataStruct.accNo = list.GetAcctNoByIndex(id);
            dataStruct.balance = list.GetBalanceByIndex(id);
            dataStruct.pin = list.GetPINByIndex(id);
            dataStruct.firstName = list.GetFirstNameByIndex(id);
            dataStruct.lastName = list.GetLastNameByIndex(id);
            dataStruct.profilePic = list.GetProfilePicByIndex(id);

            return Ok(dataStruct);
            //return NotFound();
        }
    }
}
