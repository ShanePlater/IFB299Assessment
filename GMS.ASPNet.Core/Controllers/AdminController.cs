using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.ASPNet.Core.Models.AccountViewModels;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace GMS.ASPNet.Core.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AdminController(DataContext dataContext, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Admin Home page
        /// </summary>
        /// <returns>Home Page</returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }
        public IActionResult Billing()
        {
            return View();
        }
        [Route("/Admin/Users")]
        public async Task<IActionResult> UserList()
        {
            var userVms = new List<UserViewModel>();

            foreach (var user in _dataContext.Users)
            {
                var userVm = new UserViewModel(user);
                //await userVm.GetRoles();

                userVms.Add(userVm);
            }

            return View(userVms);
        }

        [Route("/Admin/User/{id}/edit")]
        public async Task<IActionResult> EditUser(string id, string returnUrl)
        {
            return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "Account", action = "Edit", Id = id,  returnUrl }));
        }

        /// <summary>
        /// Backend action to grant roles to users
        /// </summary>
        /// <param name="id">Name of the role to be granted</param>
        /// <returns>Home Page</returns>
        public async Task<IActionResult> Grant(string id)
        {
            if (!await _roleManager.Roles.AnyAsync(role => role.NormalizedName == id.ToUpperInvariant()))
                return NotFound();

            if (!User.IsInRole(id))
                await _userManager.AddToRoleAsync(await _userManager.GetUserAsync(User), id);

            return RedirectToAction("Index");
        }

    }
}