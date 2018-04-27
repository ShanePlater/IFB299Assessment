using System;
using System.Collections.Generic;
using System.Text;

namespace GMS.Data.Models
{
    /// <summary>
    /// The time slot a teacher is available to teach
    /// </summary>
    public class Availability
    {
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime DateTime { get; set; }
    }
}
