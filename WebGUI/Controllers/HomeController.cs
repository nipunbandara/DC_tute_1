using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGUI.Models;

namespace WebGUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            try {
                string URL = "https://localhost:44369/";
                RestClient restClient = new RestClient(URL);

                RestRequest restRequest = new RestRequest("api/BankingUsers/", Method.Get);

                RestResponse restResponse = restClient.Execute(restRequest);

                List<BankingUser> users = JsonConvert.DeserializeObject<List<BankingUser>>(restResponse.Content);

                return View(users);
            }
            catch (Exception)
            {
                return View("DataBase Error");
            }
           
        }
    }
}
