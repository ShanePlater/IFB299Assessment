using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using GMS.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace GMS.ASPNet.Core.Models.AccountViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            User = new AppUser();
        }

        public UserViewModel(AppUser user)
        {
            User = user;
        }

        public AppUser User { get; }

        public Guid Id
        {
            get => User.Id;
            set => User.Id = value;
        }
        

        [Display(Name = "User Name")]
        public string UserName
        {
            get => User.UserName;
            set => User.UserName = value;
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get => User.FirstName;
            set => User.FirstName = value;
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get => User.LastName;
            set => User.LastName = value;
        }

        [Display(Name = "Email")]
        public string Email
        {
            get => User.Email;
            set => User.Email = value;
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get => User.PhoneNumber;
            set => User.PhoneNumber = value;
        }

        [Display(Name = "Teacher")]
        public bool IsTeacher { get; set; }

        [Display(Name = "Administrator")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Instruments")]
        public List<InstumentType> Instruments
        {
            get => User.Instruments;
            set => User.Instruments = value;
        }

        [InverseProperty("TaughtTo")]
        public List<Lesson> LessonsTaken
        {
            get => User.LessonsTaken;
            set => User.LessonsTaken = value;
        }

        [InverseProperty("TaughtBy")]
        public List<Lesson> LessonsTaught {
            get => User.LessonsTaught;
            set => User.LessonsTaught = value;
        }

        public List<Availability> Availabilities
        {
            get => User.Availabilities;
            set => User.Availabilities = value;
        }

        public string Address
        {
            get => User.Address;
            set => User.Address = value;
        }


    }
}
