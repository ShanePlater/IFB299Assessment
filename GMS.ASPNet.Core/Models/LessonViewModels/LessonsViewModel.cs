using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Models;

namespace GMS.ASPNet.Core.Models
{
    /// <summary>
    /// Models the Availability model to a suitable format for display on the webpage
    /// </summary>
    public class LessonsViewModel
    {
        public List<Availability> Availabilities { get; set; }


    }
}
