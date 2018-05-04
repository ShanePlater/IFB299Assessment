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

        public IActionResult getTeachers()
        {
            // Read each record and make JSON string
            // { id: 'a', title: 'Room A', eventColor: 'blue' }

            var jsonString = "[";
            foreach (var a in _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments))
            {
                jsonString += $"{{ \"id\": \"{a.UserId.ToString().Substring(0, 3)}\", \"title\" : \"{a.User.FirstName} {a.User.LastName}\", \"color\" : \"blue\" }}";
            }
            
            return Content(jsonString + "]", "application/json");

        }

        public IActionResult getAvailabilities()
        {
            // Read each record and make JSON string
            // { id: 'a', title: 'Room A', eventColor: 'blue' }

            var jsonString = "[";
            foreach (var a in _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments))
            {
                jsonString += $"{{ \"id\": \"{a.Id.ToString().Substring(0, 3)}\", \"resourceId\" : \"{a.UserId.ToString().Substring(0, 3)}\",  \"start\" : \"{a.StartTime:o}\", \"end\": \"{a.EndTime:o}\"}}";
            }

            return Content(jsonString + "]", "application/json");

        }
        /*

        [HttpGet]
        public JsonResult getTeachers()
        {          

            
            var tempString = "";
            var temp = new List<string>();

            foreach (var a in _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments))
            {
                //start time, end time, 
                tempString = $"{{id: '{a.UserId.ToString().Substring(0, 3)}', title: '{a.User.FirstName} {a.User.LastName}', allDay: false, eventColor: 'blue'}}";
                temp.Add(tempString);
            }
            return new JsonResult { Data = temp, JsonRequestBehaviour = JsonRequestBehaviour.AllowGet };

        }

        [HttpGet]
        public JsonResult getAvailabilities()
        {
           


            var tempString = "";
            var temp = new List<string>();

            foreach (var a in _context.Availabilities.Include(a => a.User).ThenInclude(u => u.Instruments))
            {
                //start time, end time, 
                tempString = $"{{id: '{a.Id.ToString().Substring(0, 3)}', resourceId: {a.UserId.ToString().Substring(30)}, start: '{a.StartTime:o}', end: '{a.EndTime:o}'}}, ";
                temp.Add(tempString);
            }
            return Json(temp);

        }
        */
    }
}