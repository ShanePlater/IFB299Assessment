using System.Collections.Generic;
using GMS.Data.Models;

namespace GMS.ASPNet.Core.Models.LessonViewModels
{
    /// <summary>
    /// Models the Availability model to a suitable format for display on the webpage
    /// </summary>
    public class LessonsViewModel
    {
        public List<Availability> Availabilities { get; set; }


    }
}
