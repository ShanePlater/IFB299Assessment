using System;
using System.Threading.Tasks;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GMS.ASPNet.Core.Controllers
{
    /// <summary>
    /// AccountController handles all actions to performed on users exluding session management such as login, logout
    /// All GMS related actions such as assigning student/teacher/admin priviledges are handled here
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger _logger;
        private readonly DataContext _dataContext;

        /// <summary>
        /// Constructor uses Dependency Injection to retrieve services registerd previously in Startup.cs
        /// </summary>
        /// <param name="userManager">ASP Net Identity UserManager instance</param>
        /// <param name="signInManager">ASP Net Identity SignInManager instance</param>
        /// <param name="context">Entity Framework inherited GMS.Data.DataContext instance</param>
        /// <param name="roleManager">ASP Net Identity RoleManager instance</param>
        /// <param name="logger">Framework provided logger for logging</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext context, RoleManager<IdentityRole<Guid>> roleManager,  ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _dataContext = context;
        }


        /// <summary>
        /// Displays the currently signed in user's profile
        /// </summary>
        /// <returns>The AppUser object to be displayed by the webpage</returns>
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
                return View(await _userManager.GetUserAsync(User));

            return RedirectToAction("Login", "Session");
        }

        public async Task<IActionResult> List()
        {
            return View(await _dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// Backend action to create roles
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Super Administrator"))
            {
                var role = new IdentityRole<Guid>
                {
                    Name = "Super Administrator",
                    
                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole<Guid>
                {
                    Name = "Administrator",

                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                var role = new IdentityRole<Guid>
                {
                    Name = "Student",

                };
                await _roleManager.CreateAsync(role);
            }
            if (!await _roleManager.RoleExistsAsync("Teacher"))
            {
                var role = new IdentityRole<Guid>
                {
                    Name = "Teacher",

                };
                await _roleManager.CreateAsync(role);
            }

            ViewData["Status"] = "Users Created";
            return View();
        }
    }
}