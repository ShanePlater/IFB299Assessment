using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GMS.ASPNet.Core.Models.LessonViewModels
{
    public class BookViewModel
    {
        public Availability _availability;

        public BookViewModel(Availability availability)
        {
            _availability = availability;
        }

        [Display(Name = "Teacher")]
        public string TeacherName => _availability.User.FirstName + " " + _availability.User.LastName;

        [DataType(DataType.Date)]
        public DateTime Date => _availability.StartTime.Date;


        [DataType(DataType.Time)]
        public DateTime Time => _availability.StartTime;


        public List<SelectListItem> Instruments => _availability.User.Instruments
            .Select(type => new SelectListItem() {Text = type.Type, Value = type.Type}).ToList();

        public object SelectedInstruments { get; set; }
    }
}
