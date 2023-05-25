using Microsoft.AspNetCore.Mvc;
using ShoppingUI.Models;
using System.Text;
using System.Text.Json;

namespace ShoppingUI.Controllers
{
    public class AccountController : Controller
    {
        HttpClient _client;
        Uri _baseAddress;
        public AccountController(IConfiguration config)
        {
            _client = new HttpClient();
            _baseAddress = new Uri(config["ApiAddress"]);
            _client.BaseAddress = _baseAddress;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            string strData = JsonSerializer.Serialize(model);
            StringContent content = new StringContent(strData,Encoding.UTF8,"application/json");
            var response = _client.PostAsync(_client.BaseAddress + "auth/validateuser", content).Result;
            if(response.IsSuccessStatusCode)
            {
                var strUser = response.Content.ReadAsStringAsync().Result;
                UserModel user = JsonSerializer.Deserialize<UserModel>(strUser, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                if(user != null)
                {
                    return RedirectToAction("Index", "Home", new { area = "User" });
                }
            }
            return View();
        }
    }
}
