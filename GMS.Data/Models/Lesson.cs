﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GMS.Data.Models
{
    public class Lesson
    {
        public Guid LessonId { get; set; }

        [Required]
        public Teacher Teacher { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public Instrument Instrument { get; set; }

        public string LessonType { get; set; }

        public int Cost { get; set; }

        public enum LessonStatus { Complete, Cancelled, Missed, Postponed };

        public LessonStatus Status { get; set; }
    }
}
