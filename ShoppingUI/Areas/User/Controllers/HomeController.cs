using Microsoft.AspNetCore.Mvc;

namespace ShoppingUI.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
