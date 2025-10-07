using Microsoft.AspNetCore.Mvc;
using Viewing.Models.Auth;

namespace Viewing.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7148/"); // your API URL
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("api/auth/login", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Dashboard", "Dashboard");

            model.ErrorMessage = "Invalid username or password.";
            return View(model);
        }
    }
}
