using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;


namespace WebGUI.Controllers
{
    public class BankingController : Controller
    {
        BankingUser user;

        public IActionResult Index()
        {
            ViewBag.Title = "Banking";
            return View();
        }

        [HttpGet]
        public IActionResult Search(int id)
        {
            try
            {
                string URL = "https://localhost:44369/";
                RestClient restClient = new RestClient(URL);

                RestRequest restRequest = new RestRequest("api/BankingUsers/{id}", Method.Get);
                restRequest.AddUrlSegment("id", id);
                RestResponse restResponse = restClient.Execute(restRequest);

                user = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);

                return Ok(restResponse.Content);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }
            

        public FileResult GetFileFromBytes(byte[] bytesIn)
        {
            return File(bytesIn, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserImageFile()
        {
            //var userPic = await user;
            if (user == null)
            {
                return NotFound();
            }

            FileResult imageUserFile = GetFileFromBytes(user.pictureData);
            return imageUserFile;
        }

        [HttpPost]
        public IActionResult Insert([FromBody] BankingUser student)
        {
            string URL = "https://localhost:44369/";

            RestClient restClient = new RestClient(URL);
            student.pictureData = System.IO.File.ReadAllBytes(@"C:\resources\1.png");
            RestRequest restRequest = new RestRequest("api/BankingUsers", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(student));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            if (returnStudent != null)
            {
                return Ok(returnStudent);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody] BankingUser student)
        {
            string URL = "https://localhost:44369/";
            RestClient restClient = new RestClient(URL);

            RestRequest restRequest = new RestRequest("api/BankingUsers/{id}", Method.Put);
            restRequest.AddUrlSegment("id", id);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(student));
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            if (returnStudent != null)
            {
                return Ok(returnStudent);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string URL = "https://localhost:44369/";
            RestClient restClient = new RestClient(URL);

            RestRequest restRequest = new RestRequest("api/BankingUsers/{id}", Method.Delete);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);

            BankingUser returnStudent = JsonConvert.DeserializeObject<BankingUser>(restResponse.Content);
            if (returnStudent != null)
            {
                return Ok(returnStudent);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
