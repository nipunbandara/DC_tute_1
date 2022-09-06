using Biz_GUI_Classes;
using Newtonsoft.Json;
using System.Threading;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class SearchController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post([FromBody] SearchData SD)
        {
            DataList list = new DataList();
            DataIntermed dataI = new DataIntermed();

            foreach (DataStruct user in list.GetDataList())
            {
                if (user.firstName == SD.searchStr)
                {
                    dataI.accNo = user.accNo;
                    dataI.balance = user.balance;
                    dataI.pin = user.pin;
                    dataI.firstName = user.firstName;
                    dataI.lastName = user.lastName;
                    dataI.profilePic = user.profilePic;

                    return Ok(dataI);
                }
            }

            Thread.Sleep(5000);

            /* DataIntermed dataI = new DataIntermed();
             DataList list = new DataList();
             dataI.accNo = list.GetAcctNoByIndex(2);
             dataI.balance = list.GetBalanceByIndex(2);
             dataI.pin = list.GetPINByIndex(2);
             dataI.firstName = SD.searchStr;
             dataI.lastName = SD.searchStr;
             dataI.profilePic = list.GetProfilePicByIndex(2);

             return Ok(dataI);*/
            return NotFound();
        }
    }
}
