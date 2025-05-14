using Microsoft.AspNetCore.Mvc;

namespace Greenhost.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
