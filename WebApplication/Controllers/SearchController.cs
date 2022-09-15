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
               
                if (user.firstName.ToLower() == SD.searchStr.ToLower())
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

            return NotFound();
        }
    }
}
