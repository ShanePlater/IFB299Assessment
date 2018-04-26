using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.ASPNet.Core.Models;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GMS.ASPNet.Core.Controllers
{
    public class LessonController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly DataContext _context;

        public LessonController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var availabilities = await _context.Availabilities.ToListAsync();
            
            return View(new LessonsViewModel(){
                Availabilities = availabilities

            });
        }

        
    }
}