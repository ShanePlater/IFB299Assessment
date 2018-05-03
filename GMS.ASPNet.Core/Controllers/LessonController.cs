using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.ASPNet.Core.Models;
using GMS.ASPNet.Core.Models.LessonViewModels;
using GMS.Data;
using GMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GMS.ASPNet.Core.Controllers
{
    [Authorize]
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
            //var availabilities = await _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments)
            //  .ToListAsync();

            return View(
                //new LessonsViewModel(){Availabilities = availabilities}
                );
        }

        public async Task<IActionResult> Book(string id)
        {
            if (id == null)
                return View();

            var user = await _userManager.GetUserAsync(User);

            var availability = await _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments).FirstOrDefaultAsync(a => a.Id == new Guid(id));

            var bookVm = new BookViewModel(availability);

            return View(bookVm);
        }

        public async Task<string> getAvailabilities()
        {
            // Read each record and make JSON string
            // { id: 'a', title: 'Room A', eventColor: 'blue' }

            var jsonString ="[";
            foreach (var a in _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments))
            {
                //start time, end time, 
                jsonString += $"{{ id: '{a.Id}', title: '{a.User.FirstName} {a.User.LastName}', start: '{a.StartTime}', end: '{a.EndTime}', eventColor: 'blue'}},";
            }
            return jsonString + "]";     
            
        }
    }
}