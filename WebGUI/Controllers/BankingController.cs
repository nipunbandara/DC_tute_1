using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class BankingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Banking Users";
            return View();
        }

        [HttpGet]
        public IActionResult Search(int id)
        {

            string URL = "https://localhost:44369/";
            RestClient restClient = new RestClient(URL);

            RestRequest restRequest = new RestRequest("api/BankingUsers/{id}", Method.Get);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] BankingUser student)
        {
            string URL = "https://localhost:44369/";
            RestClient restClient = new RestClient(URL);

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
    }
}
