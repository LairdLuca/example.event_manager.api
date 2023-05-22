using Microsoft.AspNetCore.Mvc;

namespace Event_Manager.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
