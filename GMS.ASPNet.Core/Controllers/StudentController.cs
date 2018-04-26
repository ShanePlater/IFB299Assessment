using GMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace GMS.ASPNet.Core.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}