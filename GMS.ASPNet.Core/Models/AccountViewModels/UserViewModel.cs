using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GMS.Data.Models;

namespace GMS.ASPNet.Core.Models.AccountViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Teacher")]
        public bool IsTeacher { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; }

        public List<InstumentType> Instruments { get; set; }

        [InverseProperty("TaughtTo")]
        public List<Lesson> LessonsTaken { get; set; }

        [InverseProperty("TaughtBy")]
        public List<Lesson> LessonsTaught { get; set; }

        public List<Availability> Availabilities { get; set; }

    }
}
