using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Models;

namespace GMS.ASPNet.Core.Models.LessonViewModels
{
    public class FullCalendarViewModel
    {
        public string id { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string resourceId { get; set; }

        public string color { get; set; }

        public static IEnumerable<FullCalendarViewModel> ToList(IEnumerable<Availability> availabilities)
        {
            return availabilities.Select(availability => new FullCalendarViewModel()
            {
                id = availability.Id.ToString(),
                title = availability.User.ToString(),
                start = availability.StartTime.ToString("o"),
                end = availability.EndTime.ToString("o"),
                resourceId = availability.UserId.ToString(),
                color = "blue"
            });
        }
    }
}