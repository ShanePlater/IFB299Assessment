using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GMS.ASPNet.Core.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly DataContext _dataContext;

        public AdminController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GrantTeacher()
        {
            var user = await _dataContext.Users.FindAsync(new Guid(Request.Query["UserId"]));
            if (Request.Query["Action"] == "Grant")
                user.IsTeacher = true;
            else if (Request.Query["Action"] == "Revoke")
                user.IsTeacher = false;
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("List", "Account");
        }

    }
}