using Microsoft.AspNetCore.Mvc;

namespace Viewing.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;

        protected BaseController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7148/");
        }

    }
}