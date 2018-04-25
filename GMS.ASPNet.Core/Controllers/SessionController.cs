using Microsoft.AspNetCore.Mvc;

namespace GMS.ASPNet.Core.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}