using Biz_GUI_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class GetValuesController : ApiController
    {
        public IHttpActionResult Get()
        {
            DataList list = new DataList();
            return Ok(list.GetNumRecords());
        }
            // GET api/values
            public IHttpActionResult Get(int id)
        {
            DataIntermed dataI = new DataIntermed();
            DataList list = new DataList();

            if(id <= 0 || id > list.GetNumRecords())
            {
                return NotFound();
            }
            
            dataI.accNo = list.GetAcctNoByIndex(id);
            dataI.balance = list.GetBalanceByIndex(id);
            dataI.pin = list.GetPINByIndex(id);
            dataI.firstName = list.GetFirstNameByIndex(id);
            dataI.lastName = list.GetLastNameByIndex(id);
            dataI.profilePic = list.GetProfilePicByIndex(id);

            return Ok(dataI);
        }
    }
}
