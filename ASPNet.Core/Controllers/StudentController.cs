using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNet.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNet.Core.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var dataService = new DataService();
            return View(dataService.getStudents());
        }
    }
}