using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Viewing.Controllers.Dashboard
{
    public class DepartmentsController : Controller
    {
        private readonly HttpClient _httpClient;

        public DepartmentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7148/"); 
        }

        [HttpGet]
        public IActionResult Departments()
        {
            return PartialView();
;
        }
    }
}