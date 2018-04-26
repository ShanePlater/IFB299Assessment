using System;
using System.Threading.Tasks;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GMS.ASPNet.Core.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private DataContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context,  ILogger<SessionController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
                return View(await _userManager.GetUserAsync(User));

            return RedirectToAction("Login", "Session");
        }

        public async Task<IActionResult> Roles()
        {
            if (!await _roleManager.RoleExistsAsync("Super Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Super Administrator",
                    
                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator",

                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                var role = new IdentityRole
                {
                    Name = "Super Administrator",

                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Teacher"))
            {
                var role = new IdentityRole
                {
                    Name = "Super Administrator",

                };
                await _roleManager.CreateAsync(role);
            }

            ViewData["Status"] = "Users Created";
            return View();
        }
    }
}