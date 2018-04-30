using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GMS.Data.Models
{
    /// <summary>
    /// The time slot a teacher is available to teach
    /// </summary>
    public class Availability
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public AppUser User { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}
