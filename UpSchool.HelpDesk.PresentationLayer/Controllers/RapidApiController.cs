using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UpSchool.HelpDesk.PresentationLayer.Models;

namespace UpSchool.HelpDesk.PresentationLayer.Controllers
{
    public class RapidApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(string country)
        {
            var client = new HttpClient();

            var model = new RapidApiResultModel();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-193.p.rapidapi.com/statistics?country={country}"),
                Headers =
    {
        { "X-RapidAPI-Key", "1df7bb17f3msh5618e48a55c4688p1b8dfajsn0e0862e6451f" },
        { "X-RapidAPI-Host", "covid-193.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
               model =  JsonConvert.DeserializeObject<RapidApiResultModel>(body);
            }
            return View(model);
        }
    }
}
