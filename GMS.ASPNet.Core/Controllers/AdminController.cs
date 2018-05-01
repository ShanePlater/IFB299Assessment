using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AdminController(DataContext dataContext, UserManager<AppUser> userManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
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
            return RedirectToAction("ListUsers", "Admin");
        }

        public async Task<IActionResult> GrantAdmin()
        {
            if (!User.IsInRole("Administrator"))
                await _userManager.AddToRoleAsync(await _userManager.GetUserAsync(User), "Administrator");

            return View();
        }
        public async Task<IActionResult> ListUsers()
        {
            return View(await _dataContext.Users.ToListAsync());
        }

        public async Task<IActionResult> EditUser(string id, string returnURL)
        {// need to change this to reroute to accountcontroller edit
            return RedirectToAction("Edit", new RouteValueDictionary(new { controller = "Account", action = "Edit", Id = id, returnUrl = returnURL}));
        }
       

        public async Task<IActionResult> Seed()
        {
            SeedUsers();

            return View();
        }


        private async void SeedUsers()
        {
            var pwd = "Test1234";

            var mika = new AppUser()
            {
                FirstName = "Mika",
                LastName = "Mika",
                Email = "mika@gms.com.au",
                UserName = "mika",
                Instruments = new List<InstumentType>()
                {
                    new InstumentType(){Type = "Violin"},
                    new InstumentType(){Type = "Piano"},
                    new InstumentType(){Type = "Thabla"},
                }
            };

            var aadil = new AppUser()
            {
                FirstName = "Aadil",
                LastName = "Abdullah",
                Email = "avinkavish@gmail.com",
                UserName = "aadil",
                Instruments = new List<InstumentType>()
                {
                    new InstumentType(){Type = "Guitar"},
                }
            };

            var avin = new AppUser()
            {
                FirstName = "Avin",
                LastName = "Abeyratne",
                Email = "avinkavish@gmail.com",
                UserName = "avin"
            };

            var eric = new AppUser()
            {
                FirstName = "Eric",
                LastName = "Tang",
                Email = "eric@tang.com",
                UserName = "eric"
            };

            var shane = new AppUser()
            {
                FirstName = "Shane",
                LastName = "Plater",
                Email = "shane@plater.com",
                UserName = "shane"
            };

            var student1 = new AppUser()
            {
                FirstName = "George",
                LastName = "Washington",
                Email = "george.washington@whitehouse.gov",
                UserName = "georgey",
                Instruments = new List<InstumentType>()
                {
                    new InstumentType(){Type = "Guitar"},
                }

            };

            var student2 = new AppUser()
            {
                FirstName = "Barack",
                LastName = "Osama",
                Email = "b.boy@whitehouse.gov",
                UserName = "barrel",
                Instruments = new List<InstumentType>()
                {
                    new InstumentType(){Type = "Piano"},
                }
            };

            var student3 = new AppUser()
            {
                FirstName = "Barack",
                LastName = "Osama",
                Email = "b.boy@whitehouse.gov",
                UserName = "barrel",
                Instruments = new List<InstumentType>()
                {
                    new InstumentType(){Type = "Chainsaw"},
                }
            };

            await _userManager.CreateAsync(mika, pwd);
            await _userManager.AddToRoleAsync(mika, "Super Administrator");

            await _userManager.CreateAsync(aadil, pwd);
            await _userManager.AddToRoleAsync(aadil, "Administrator");

            await _userManager.CreateAsync(avin, pwd);
            await _userManager.AddToRoleAsync(avin, "Administrator");

            await _userManager.CreateAsync(eric, pwd);
            await _userManager.AddToRoleAsync(eric, "Administrator");

            await _userManager.CreateAsync(shane, pwd);
            await _userManager.AddToRoleAsync(shane, "Administrator");

            await _userManager.CreateAsync(student1, pwd);
            await _userManager.AddToRoleAsync(student1, "Student");

            await _userManager.CreateAsync(student2, pwd);
            await _userManager.AddToRoleAsync(student2, "Student");

            await _dataContext.SaveChangesAsync();

        }

    }
}