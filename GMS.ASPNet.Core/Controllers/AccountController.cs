using System;
using System.Linq;
using System.Threading.Tasks;
using GMS.ASPNet.Core.Models.AccountViewModels;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Session");

            var user = await _userManager.GetUserAsync(User);
            var userVm = new UserViewModel(user);
            ;
            return View(userVm);

        }

        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (id == null)
                return NotFound();

            var user = await GetFullUser(id);

            if (user == null)
                return NotFound();

            var userVm = new UserViewModel(user);

            var selectList = user.Instruments.Select(
                type => new SelectListItem()
                {
                    Text = type.Type,
                    Value = type.Type,
                    Selected = false
                });

            userVm.SelectInstruments = selectList;
            await GetRoles(user, userVm);
            return View(userVm);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(UserViewModel model, string returnUrl = null)
        {
            if (model == null) NotFound();

            ViewData["ReturnUrl"] = returnUrl;
            if (model == null)
                return NotFound();

            var user = await GetFullUser(model.Id.ToString());

            if (user == null)
                return NotFound();

            var userVm = new UserViewModel(user);

            var selectList = user.Instruments.Select(
                type => new SelectListItem()
                {
                    Text = type.Type,
                    Value = type.Type,
                    Selected = false
                });

            userVm.SelectInstruments = selectList;

            if (!ModelState.IsValid)
                return View(userVm);

            UpdateValues(user, model);
            await SetRoles(user, model);
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateNormalizedEmailAsync(user);
            await _userManager.UpdateNormalizedUserNameAsync(user);
            ViewData["Status"] = "Changes Saved";

            return View(userVm);
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

            ViewData["Status"] = "Roles Created";
            return View();
        }


        public async Task<bool> SetRoles(AppUser user, UserViewModel model)
        {
            if (model.IsTeacher)
                await _userManager.AddToRoleAsync(user, "Teacher");
            else
                await _userManager.RemoveFromRoleAsync(user, "Teacher");

            if (model.IsAdmin)
                await _userManager.AddToRoleAsync(user, "Administrator");
            else
                await _userManager.RemoveFromRoleAsync(user, "Administrator");

            return true;
        }


        public void UpdateValues(AppUser user, UserViewModel model)
        {
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName.ToUpperInvariant();
            user.NormalizedEmail = model.Email.ToUpperInvariant();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
        }

        public async Task<bool> GetRoles(AppUser user, UserViewModel model)
        {
            model.IsTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
            model.IsAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            return true;
        }

        private async Task<AppUser> GetFullUser(string id)
        {
            var user = await _dataContext.Users.Include(u => u.Instruments)
                .Include(u => u.Availabilities)
                .Include(u => u.LessonsTaken)
                .Include(u => u.LessonsTaught)
                .FirstOrDefaultAsync(u => u.Id == new Guid(id));
            return user;
        }
    }
}