using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GMS.Data.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GMS.ASPNet.Core.Models.LessonViewModels
{
    public class AvailabilityViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public AppUser User { get; set; }    

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
