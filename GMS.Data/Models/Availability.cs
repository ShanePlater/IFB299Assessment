using System;
using System.Collections.Generic;
using System.Text;

namespace GMS.Data.Models
{
    public class Availability
    {
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public DateTime DateTime { get; set; }
    }
}
