using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Viewing.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _httpClient;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7148/"); 
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View(); 
        }

        // POST example (optional)
        [HttpPost]
        public IActionResult DashboardPost()
        {
            return View("Dashboard"); 
        }
    }
}